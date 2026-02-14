using UnityEngine;
using UnityEngine.Pool;

namespace PhikozzLibrary
{
    public interface IPoolService
    {
        void CreatePool<T>(T prefab, int defaultCapacity, int maxSize) where T : MonoBehaviour, IPoolObject;
        ObjectPool<T> Get<T>(T prefab) where T : MonoBehaviour, IPoolObject;
        void Release<T>(T prefab) where T : MonoBehaviour, IPoolObject;
        void ReleaseAll<T>(T prefab) where T : MonoBehaviour, IPoolObject;
        void DestroyAll<T>(T prefab) where T : MonoBehaviour, IPoolObject;
        ObjectPool<T> GetPool<T>(T prefab) where T : MonoBehaviour, IPoolObject;
    }
}