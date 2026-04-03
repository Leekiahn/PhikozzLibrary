using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace PhikozzLibrary
{
    public class ResourceManager : SingletonGlobal<ResourceManager>, IResourceService, IInitializable
    {
        private Dictionary<string, AsyncOperationHandle> _loadedHandles = new();
        private Dictionary<string, List<AsyncOperationHandle>> _loadedHandleLists = new();

        public bool Init()
        {
            try
            {
                ServiceLocator.Register<IResourceService>(this);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return false;
            }
        }

        public async UniTask<T> LoadAsync<T>(string key) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedHandles[key] = handle;
                return handle.Result;
            }

            return null;
        }

        public async UniTask<List<T>> LoadAllAsync<T>(string label) where T : Object
        {
            var handle = Addressables.LoadAssetsAsync<T>(label, null);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedHandleLists[label] = new List<AsyncOperationHandle> { handle };
                return new List<T>(handle.Result);
            }

            return null;
        }

        public void ReleaseByKey(string key)
        {
            if (_loadedHandles.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _loadedHandles.Remove(key);
            }
        }

        public void ReleaseByLabel(string label)
        {
            if (_loadedHandleLists.TryGetValue(label, out var handleList))
            {
                foreach (var handle in handleList)
                {
                    Addressables.Release(handle);
                }
                _loadedHandleLists.Remove(label);
            }
        }
    }
}