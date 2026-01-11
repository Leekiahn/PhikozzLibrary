using System.Collections.Generic;
using PhikozzLibrary;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// 리소스 관리 매니저
/// Addressable Asset System 미사용 시 Resources 폴더 기반 리소스 로드 관리
/// </summary>
public class ResourceManager : GenericSingleton<ResourceManager>
{
    #region >--------------------------------------------- fields & Properties

    private Dictionary<string, Object> _cache = new Dictionary<string, Object>(); // 단일 오브젝트 캐시
    private Dictionary<string, Object[]> _cacheAll = new Dictionary<string, Object[]>(); // 다중 오브젝트 캐시

    #endregion

    #region >--------------------------------------------- Load & Unload (sync)

    /// <summary>
    /// 리소스 단일 동기 로드
    /// </summary>
    public T Load<T>(string path) where T : Object
    {
        path = NormalizeResourcePath(path);
        if (_cache.TryGetValue(path, out var obj))
        {
            return obj as T;
        }

        var asset = Resources.Load<T>(path);
        if (asset != null)
        {
            _cache[path] = asset;
        }
        return asset;
    }

    /// <summary>
    /// 리소스 다중 동기 로드
    /// </summary>
    public T[] LoadAll<T>(string[] paths) where T : Object
    {
        var results = new T[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            results[i] = Load<T>(paths[i]);
        }
        return results;
    }

    #endregion

    #region >--------------------------------------------- Load & Unload (async)

    /// <summary>
    /// 리소스 단일 로드
    /// </summary>
    /// <param name="path">파일 경로 (기본값: Assets/Resources)</param>
    /// <typeparam name="T">유니티 오브젝트 타입</typeparam>
    /// <returns>로드된 오브젝트</returns>
    public async Task<T> LoadAsync<T>(string path) where T : Object
    {
        path = NormalizeResourcePath(path);
        if (_cache.TryGetValue(path, out var obj))
        {
            return obj as T;
        }

        var request = Resources.LoadAsync<T>(path);
        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.asset != null)
        {
            _cache[path] = request.asset;
        }

        return request.asset as T;
    }

    /// <summary>
    /// 리소스 다중 로드
    /// </summary>
    /// <param name="path">파일 경로 (기본값: Assets/Resources)</param>
    /// <typeparam name="T">유니티 오브젝트 타입</typeparam>
    /// <returns>로드된 오브젝트 배열</returns>
    public async Task<T[]> LoadAllAsync<T>(string[] paths) where T : Object
    {
        var tasks = new List<Task<T>>();
        foreach (var path in paths)
        {
            tasks.Add(LoadAsync<T>(path));
        }

        var results = await Task.WhenAll(tasks);
        return results;
    }

    /// <summary>
    /// 리소스 언로드
    /// </summary>
    /// <param name="path">파일 경로 (기본값: Assets/Resources)</param>
    public void Unload(string path)
    {
        path = NormalizeResourcePath(path);
        if (_cache.TryGetValue(path, out var obj))
        {
            Resources.UnloadAsset(obj);
            _cache.Remove(path);
        }
    }

    /// <summary>
    /// 리소스 모두 언로드
    /// </summary>
    public void UnloadAll()
    {
        foreach (var obj in _cache.Values)
        {
            Resources.UnloadAsset(obj);
        }

        _cache.Clear();
    }

    /// <summary>
    /// Resources 경로를 항상 Resources 하위 상대경로로 변환
    /// </summary>
    private string NormalizeResourcePath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        // 앞에 Assets/Resources/ 또는 Resources/가 있으면 제거
        if (path.StartsWith("Assets/Resources/"))
            path = path.Substring("Assets/Resources/".Length);
        else if (path.StartsWith("Resources/"))
            path = path.Substring("Resources/".Length);
        // 확장자 제거
        int ext = path.LastIndexOf('.');
        if (ext > 0)
            path = path.Substring(0, ext);
        return path;
    }

    #endregion
}