using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public interface IResourceService
{
    UniTask<T> LoadAsync<T>(string path) where T : Object;
    UniTask<List<T>> LoadAllAsync<T>(string label) where T : Object;
    void Unload(string key);
    void UnloadHandleList(string label);
}