using UnityEngine;

namespace PhikozzLibrary
{
    public interface IPoolService
    {
        void CreatePool<T>(T prefab, Transform parent, int size) where T : BasePoolObject;
        void Spawn<T>(T prefab) where T : BasePoolObject;
        void Spawn<T>(T prefab, int count) where T : BasePoolObject;
        void Despawn<T>(T prefab) where T : BasePoolObject;
        void Despawn<T>(T prefab, int count) where T : BasePoolObject;
        void DespawnAll<T>(T prefab) where T : BasePoolObject;
        void DestroyPool<T>(T prefab) where T : BasePoolObject;
    }
}