using System.Collections.Generic;
using PhikozzLibrary;
using UnityEngine;

/// <summary>
/// 리소스 관리 매니저
/// </summary>
public class ResourceManager : GenericSingleton<ResourceManager>
{
    #region >--------------------------------------------- fields & Properties

    private Dictionary<string, Object> _cache = new Dictionary<string, Object>();   // 단일 오브젝트 캐시
    private Dictionary<string, Object[]> _cacheAll = new Dictionary<string, Object[]>();    // 다중 오브젝트 캐시

    #endregion

    #region >--------------------------------------------- Load & Unload

    /// <summary>
    /// 리소스 로드
    /// </summary>
    /// <param name="path">파일 경로 (기본값: Assets/Resources)</param>
    /// <typeparam name="T">유니티 오브젝트 타입</typeparam>
    /// <returns>로드된 오브젝트</returns>
    public T Load<T>(string path) where T : Object
    {
        path = NormalizeResourcePath(path);
        if (_cache.TryGetValue(path, out var obj))
        {
            return obj as T;
        }
        var loaded = Resources.Load<T>(path);
        if (loaded != null)
        {
            _cache[path] = loaded;
        }
        return loaded;
    }
    
    /// <summary>
    /// 리소스 다중 로드
    /// </summary>
    /// <param name="path">파일 경로 (기본값: Assets/Resources)</param>
    /// <typeparam name="T">유니티 오브젝트 타입</typeparam>
    /// <returns>로드된 오브젝트 배열</returns>
    public T[] LoadAll<T>(string path) where T : Object
    {
        path = NormalizeResourcePath(path);
        if (_cacheAll.TryGetValue(path, out var objs))
        {
            return objs as T[];
        }
        var loadedArray = Resources.LoadAll<T>(path);
        if (loadedArray != null && loadedArray.Length > 0)
        {
            _cacheAll[path] = loadedArray;
            return loadedArray;
        }
        return null;
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