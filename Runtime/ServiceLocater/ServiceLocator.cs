using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    /// <summary>
    /// 서비스 로케이터
    /// </summary>
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"{type.Name} 서비스가 이미 등록되어 있습니다.");
                return;
            }
            _services[type] = service;
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return service as T;
            }
            Debug.LogWarning($"{type.Name} 서비스가 등록되어 있지 않습니다.");
            return null;
        }

        public static void Unregister<T>() where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }
    }
}
