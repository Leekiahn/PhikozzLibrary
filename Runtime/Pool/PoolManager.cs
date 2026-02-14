using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace PhikozzLibrary
{
    public class PoolManager : SingletonGlobal<PoolManager>, IPoolService
    {
        private Dictionary<string, object> _pools = new Dictionary<string, object>();

        public ObjectPool<T> Get<T>(T prefab) where T : MonoBehaviour, IPoolObject
        {
            string key = prefab.GetType().Name;
            if (!_pools.ContainsKey(key))
            {
                _pools[key] = new ObjectPool<T>(
                    createFunc: () => Instantiate(prefab),
                    actionOnGet: obj => obj.gameObject.SetActive(true),
                    actionOnRelease: obj => obj.gameObject.SetActive(false),
                    actionOnDestroy: obj => Destroy(obj.gameObject),
                    collectionCheck: false,
                    defaultCapacity: 10,
                    maxSize: 100
                );
            }
            return (ObjectPool<T>)_pools[key];
        }
    }
}
