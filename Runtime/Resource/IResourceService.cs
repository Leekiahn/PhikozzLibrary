using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

namespace PhikozzLibrary
{
    public interface IResourceService
    {
        UniTask<T> LoadAsync<T>(string path) where T : Object;
        UniTask<List<T>> LoadAllAsync<T>(string label) where T : Object;
        void ReleaseByKey(string key);
        void ReleaseByLabel(string label);
    }
}
