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
        private readonly Dictionary<string, AsyncOperationHandle> _assetHandles = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, AsyncOperationHandle> _labelHandles = new Dictionary<string, AsyncOperationHandle>();
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
        
        public async UniTask<T> Load<T>(string key) where T : Object
        {
            if (_assetHandles.TryGetValue(key, out AsyncOperationHandle handle))
            {
                return handle.Result as T;
            }
            
            handle = Addressables.LoadAssetAsync<T>(key);
            await handle.ToUniTask();
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _assetHandles[key] = handle;
                return handle.Result as T;
            }
            
            return null;
        }

        public async UniTask<List<T>> LoadLabel<T>(string label) where T : Object
        {
            if (_labelHandles.TryGetValue(label, out AsyncOperationHandle handle))
            {
                return handle.Result as List<T>;
            }
            
            handle = Addressables.LoadAssetsAsync<T>(label, null);
            await handle.ToUniTask();
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _labelHandles[label] = handle;
                return handle.Result as List<T>;
            }
            
            return null;
        }
        
        public async UniTask Unload(string key)
        {
            if (_assetHandles.TryGetValue(key, out AsyncOperationHandle handle))
            {
                Addressables.Release(handle);
                _assetHandles.Remove(key);
            }
        }

        public async UniTask UnloadLabel(string label)
        {
            if (_labelHandles.TryGetValue(label, out AsyncOperationHandle handle))
            {
                Addressables.Release(handle);
                _labelHandles.Remove(label);
            }
        }
        
        public UniTask UnloadAll()
        {
            List<AsyncOperationHandle> assetHandles = new List<AsyncOperationHandle>(_assetHandles.Values);
            List<AsyncOperationHandle> labelHandles = new List<AsyncOperationHandle>(_labelHandles.Values);

            for (int i = 0; i < assetHandles.Count; i++)
            {
                Addressables.Release(assetHandles[i]);
            }

            for (int i = 0; i < labelHandles.Count; i++)
            {
                Addressables.Release(labelHandles[i]);
            }

            _assetHandles.Clear();
            _labelHandles.Clear();

            return UniTask.CompletedTask;
        }
    }
}