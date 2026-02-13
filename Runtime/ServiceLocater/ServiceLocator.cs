using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    /// <summary>
    /// 서비스 로케이터 싱글톤
    /// </summary>
    public class ServiceLocator : GenericSingleton<ServiceLocator>
    {
        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// 서비스 등록
        /// </summary>
        public void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"{type.Name} 서비스가 이미 등록되어 있습니다.");
                return;
            }
            _services[type] = service;
        }

        /// <summary>
        /// 서비스 조회
        /// </summary>
        public T Get<T>() where T : class
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return service as T;
            }
            Debug.LogWarning($"{type.Name} 서비스가 등록되어 있지 않습니다.");
            return null;
        }

        /// <summary>
        /// 서비스 제거
        /// </summary>
        public void Unregister<T>() where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }
    }
}

