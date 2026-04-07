using UnityEngine;

namespace PhikozzLibrary
{
    public interface IPoolService
    {
        void CreatePool<T>(string key, T prefab, Transform parent, int size) where T : BasePoolObject;
        T Spawn<T>(string key) where T : BasePoolObject;
        T Spawn<T>(string key, Vector3 position) where T : BasePoolObject;
        T Spawn<T>(string key, Vector3 position, Vector3 rotation) where T : BasePoolObject;
        void Despawn(string key);
        void DespawnAll(string key);
        void DestroyPool(string key);
    }
}