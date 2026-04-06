using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    public class EventManager : SingletonGlobal<EventManager>, IEventService, IInitializable
    {
        private readonly Dictionary<Type, Delegate> _eventTable = new Dictionary<Type, Delegate>();


        public bool Init()
        {
            try
            {
                ServiceLocator.Register<IEventService>(this);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return false;
            }
        }


        public void Subscribe<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            if (_eventTable.TryGetValue(type, out var del))
            {
                _eventTable[type] = Delegate.Combine(del, onEvent);
            }
            else
            {
                _eventTable[type] = onEvent;
            }
        }


        public void Unsubscribe<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            if (_eventTable.TryGetValue(type, out var del))
            {
                var currentDel = Delegate.Remove(del, onEvent);
                if (currentDel == null)
                {
                    _eventTable.Remove(type);
                }
                else
                {
                    _eventTable[type] = currentDel;
                }
            }
        }


        public void UnSubscribeAll<T>()
        {
            var type = typeof(T);
            _eventTable.Remove(type);
        }


        public void Publish<T>(T eventData)
        {
            var type = typeof(T);
            if (_eventTable.TryGetValue(type, out var del))
            {
                (del as Action<T>)?.Invoke(eventData);
            }
        }
    }
}