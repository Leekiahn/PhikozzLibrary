using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

namespace PhikozzLibrary
{
    public interface IResourceService
    {
        public UniTask<T> Load<T>(string key) where T : Object;
        public UniTask<List<T>> LoadLabel<T>(string label) where T : Object;
    }
}
