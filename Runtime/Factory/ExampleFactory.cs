using static UnityEngine.Object;
using UnityEngine;
using PhikozzLibrary.Runtime.Factory;

public class ExampleFactory<T> : IFactory<T> where T : Component
{
    public T Create(T prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }
}