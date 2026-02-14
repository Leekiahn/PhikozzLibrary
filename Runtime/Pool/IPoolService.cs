using UnityEngine;
using UnityEngine.Pool;

namespace PhikozzLibrary
{
    public interface IPoolService
    {
        ObjectPool<T> Get<T>(T prefab) where T : MonoBehaviour, IPoolObject;
        //void Release<T>(T obj) where T : MonoBehaviour, IPoolObject;
        //void ReleaseAll<T>(T prefab) where T : MonoBehaviour, IPoolObject;
        //void DestroyAll<T>(T prefab) where T : MonoBehaviour, IPoolObject;
    }
}