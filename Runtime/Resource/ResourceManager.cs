using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using PhikozzLibrary;

public class ResourceManager : SingletonGlobal<ResourceManager>, IResourceService, IPreinitialize
{
    private Dictionary<string, AsyncOperationHandle> _loadedHandles = new();
    private Dictionary<string, List<AsyncOperationHandle>> _loadedHandleLists = new();

    public UniTask<bool> InitAsync()
    {
        try
        {
            ServiceLocator.Register<IResourceService>(this);
            return UniTask.FromResult(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("저장 시스템 초기화 실패: " + ex.Message);
            return UniTask.FromResult(false);
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

        Debug.LogError($"리소스 로드 실패: {key}");
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

        Debug.LogError($"리소스 로드 실패: {label}");
        return null;
    }

    public void ReleaseByKey(string key)
    {
        if (_loadedHandles.TryGetValue(key, out var handle))
        {
            Addressables.Release(handle);
            _loadedHandles.Remove(key);
            Debug.Log("메모리에서 리소스 해제함: " + key);
        }
        else
        {
            Debug.LogWarning($"해제할 리소스가 없습니다: {key}");
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
            Debug.Log("메모리에서 해당 레이블의 리소스 해제함: " + label);
        }
        else
        {
            Debug.LogWarning($"해제할 레이블의 리소스가 없습니다: {label}");
        }
    }
}
