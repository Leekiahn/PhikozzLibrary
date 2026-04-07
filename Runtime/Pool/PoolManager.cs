using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    public class PoolManager : SingletonGlobal<PoolManager>, IPoolService, IInitializable
    {
        private readonly Dictionary<Type, Queue<BasePoolObject>> _pools = new Dictionary<Type, Queue<BasePoolObject>>();
        private readonly Dictionary<Type, List<BasePoolObject>> _activePools = new Dictionary<Type, List<BasePoolObject>>();
        
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

        public void CreatePool<T>(T prefab, Transform parent, int size) where T : BasePoolObject
        {
            Queue<BasePoolObject> pool = new Queue<BasePoolObject>(size);
            List<BasePoolObject> activeList = new List<BasePoolObject>();

            for (int i = 0; i < size; i++)
            {
                T instance = Instantiate(prefab, parent);
                instance.OnCreate();
                pool.Enqueue(instance);
            }

            _pools.Add(typeof(T), pool);
            _activePools.Add(typeof(T), activeList);
        }
        

        public void Spawn<T>(T prefab) where T : BasePoolObject
        {
            if (_pools.TryGetValue(typeof(T), out Queue<BasePoolObject> pool) && pool.Count > 0)
            {
                BasePoolObject obj = pool.Dequeue();
                obj.OnSpawn();
                _activePools[typeof(T)].Add(obj);
            }
        }

        public void Spawn<T>(T prefab, int count) where T : BasePoolObject
        {
            if (_pools.TryGetValue(typeof(T), out Queue<BasePoolObject> pool) && pool.Count >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    BasePoolObject obj = pool.Dequeue();
                    obj.OnSpawn();
                    _activePools[typeof(T)].Add(obj);
                }
            }
        }


        public void Despawn<T>(T prefab) where T : BasePoolObject
        {
            if (_activePools.TryGetValue(typeof(T), out List<BasePoolObject> activeList) && activeList.Count > 0)
            {
                int lastIndex = activeList.Count - 1;
                BasePoolObject obj = activeList[lastIndex];
                activeList.RemoveAt(lastIndex);

                obj.OnDespawn();
                _pools[typeof(T)].Enqueue(obj);
            }
        }

        public void Despawn<T>(T prefab, int count) where T : BasePoolObject
        {
            if (_activePools.TryGetValue(typeof(T), out List<BasePoolObject> activeList) && activeList.Count >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    int lastIndex = activeList.Count - 1;
                    BasePoolObject obj = activeList[lastIndex];
                    activeList.RemoveAt(lastIndex);

                    obj.OnDespawn();
                    _pools[typeof(T)].Enqueue(obj);
                }
            }
        }


        public void DespawnAll<T>(T prefab) where T : BasePoolObject
        {
            if (_activePools.TryGetValue(typeof(T), out List<BasePoolObject> activeList))
            {
                for (int i = activeList.Count - 1; i >= 0; i--)
                {
                    BasePoolObject obj = activeList[i];
                    obj.OnDespawn();
                    _pools[typeof(T)].Enqueue(obj);
                }

                activeList.Clear();
            }
        }


        public void DestroyPool<T>(T prefab) where T : BasePoolObject
        {
            if (_pools.TryGetValue(typeof(T), out Queue<BasePoolObject> pool))
            {
                while (pool.Count > 0)
                {
                    BasePoolObject obj = pool.Dequeue();
                    obj.OnDestroy();
                    Destroy(obj.gameObject);
                }

                _pools.Remove(typeof(T));
            }

            if (_activePools.TryGetValue(typeof(T), out List<BasePoolObject> activeList))
            {
                foreach (BasePoolObject obj in activeList)
                {
                    obj.OnDestroy();
                    Destroy(obj.gameObject);
                }

                _activePools.Remove(typeof(T));
            }
        }
    }
}
