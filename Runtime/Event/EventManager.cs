using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PhikozzLibrary;

public class EventManager : SingletonGlobal<EventManager>, IEventService
{
    private readonly Dictionary<Type, Delegate> _eventTable = new Dictionary<Type, Delegate>();

    public UniTask<bool> InitAsync()
    {
            ServiceLocator.Register<IEventService>(this);
        return UniTask.FromResult(true);
    }
    
    public void Subscribe<T>(Action<T> onEvent) where T : IGameEvent
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var del))
            _eventTable[type] = Delegate.Combine(del, onEvent);
        else
            _eventTable[type] = onEvent;
    }

    public void Unsubscribe<T>(Action<T> onEvent) where T : IGameEvent
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var del))
        {
            var currentDel = Delegate.Remove(del, onEvent);
            if (currentDel == null)
                _eventTable.Remove(type);
            else
                _eventTable[type] = currentDel;
        }
    }

    public void Publish<T>(T eventData) where T : IGameEvent
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var del))
        {
            (del as Action<T>)?.Invoke(eventData);
        }
    }
}