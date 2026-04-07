using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    public class PoolManager : SingletonGlobal<PoolManager>, IPoolService, IInitializable
    {
        private readonly Dictionary<string, Queue<BasePoolObject>> _pools = new Dictionary<string, Queue<BasePoolObject>>();
        private readonly Dictionary<string, List<BasePoolObject>> _activePools = new Dictionary<string, List<BasePoolObject>>();
        
        public bool Init()
        {
            try
            {
                ServiceLocator.Register<IPoolService>(this);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return false;
            }
        }

        public void CreatePool<T>(string key, T prefab, Transform parent, int size) where T : BasePoolObject
        {
            if  (_pools.ContainsKey(key))
            {
                return;
            }
            
            Queue<BasePoolObject> pool = new Queue<BasePoolObject>(size);
            List<BasePoolObject> activeList = new List<BasePoolObject>();

            for (int i = 0; i < size; i++)
            {
                T instance = Instantiate(prefab, parent);
                instance.OnCreate();
                pool.Enqueue(instance);
            }

            _pools.Add(key, pool);
            _activePools.Add(key, activeList);
        }


        public T Spawn<T>(string key) where T : BasePoolObject
        {
            if (_pools.TryGetValue(key, out Queue<BasePoolObject> pool) && pool.Count > 0)
            {
                BasePoolObject obj = pool.Dequeue();
                obj.OnSpawn();
                _activePools[key].Add(obj);
                
                return (T)obj;
            }
            else
            {
                return null;
            }
        }

        public T Spawn<T>(string key, Vector3 position) where T : BasePoolObject
        {
            if (_pools.TryGetValue(key, out Queue<BasePoolObject> pool) && pool.Count > 0)
            {
                BasePoolObject obj = pool.Dequeue();
                obj.OnSpawn();
                obj.transform.position = position;
                _activePools[key].Add(obj);
                
                return (T)obj;
            }
            else
            {
                return null;
            }
        }

        public T Spawn<T>(string key, Vector3 position, Vector3 rotation) where T : BasePoolObject
        {
            if (_pools.TryGetValue(key, out Queue<BasePoolObject> pool) && pool.Count > 0)
            {
                BasePoolObject obj = pool.Dequeue();
                obj.OnSpawn();
                obj.transform.position = position;
                obj.transform.rotation = Quaternion.Euler(rotation);
                _activePools[key].Add(obj);
                
                return (T)obj;
            }
            else
            {
                return null;
            }
        }


        public void Despawn(string key)
        {
            if (_activePools.TryGetValue(key, out List<BasePoolObject> activeList) && activeList.Count > 0)
            {
                int lastIndex = activeList.Count - 1;
                BasePoolObject obj = activeList[lastIndex];
                activeList.RemoveAt(lastIndex);

                obj.OnDespawn();
                _pools[key].Enqueue(obj);
            }
        }


        public void DespawnAll(string key)
        {
            if (_activePools.TryGetValue(key, out List<BasePoolObject> activeList))
            {
                for (int i = activeList.Count - 1; i >= 0; i--)
                {
                    BasePoolObject obj = activeList[i];
                    obj.OnDespawn();
                    _pools[key].Enqueue(obj);
                }

                activeList.Clear();
            }
        }


        public void DestroyPool(string key)
        {
            if (_pools.TryGetValue(key, out Queue<BasePoolObject> pool))
            {
                while (pool.Count > 0)
                {
                    BasePoolObject obj = pool.Dequeue();
                    obj.OnDestroy();
                    Destroy(obj.gameObject);
                }

                _pools.Remove(key);
            }

            if (_activePools.TryGetValue(key, out List<BasePoolObject> activeList))
            {
                foreach (BasePoolObject obj in activeList)
                {
                    obj.OnDestroy();
                    Destroy(obj.gameObject);
                }

                _activePools.Remove(key);
            }
        }
    }
}
