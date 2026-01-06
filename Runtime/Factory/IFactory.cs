using UnityEngine;

namespace PhikozzLibrary.Runtime.Factory
{
    public interface IFactory<T> where T : Component
    {
        T Create(T prefab, Vector3 position, Quaternion rotation);
    }
}