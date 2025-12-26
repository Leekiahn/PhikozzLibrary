using System.Collections.Generic;
using PhikozzLibrary;
using UnityEngine;

public class ResourceManager : GenericSingleton<ResourceManager>
{
    #region >--------------------------------------------- fields & Properties

    private static Dictionary<string, Object> _cache = new Dictionary<string, Object>();   // 단일 오브젝트 캐시
    private static Dictionary<string, Object[]> _cacheAll = new Dictionary<string, Object[]>();    // 다중 오브젝트 캐시

    #endregion

    #region >--------------------------------------------- Load & Unload

    /// <summary>
    /// 리소스 로드
    /// </summary>
    /// <param name="path">파일 경로</param>
    /// <typeparam name="T">유니티 오브젝트 타입</typeparam>
    /// <returns>로드된 오브젝트</returns>
    public static T Load<T>(string path) where T : Object
    {
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
    /// <param name="path">파일 경로</param>
    /// <typeparam name="T">유니티 오브젝트 타입</typeparam>
    /// <returns>로드된 오브젝트 배열</returns>
    public static T[] LoadAll<T>(string path) where T : Object
    {
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
    /// <param name="path">파일 경로</param>
    public static void Unload(string path)
    {
        if (_cache.TryGetValue(path, out var obj))
        {
            Resources.UnloadAsset(obj);
            _cache.Remove(path);
        }
    }

    /// <summary>
    /// 리소스 모두 언로드
    /// </summary>
    public static void UnloadAll()
    {
        foreach (var obj in _cache.Values)
        {
            Resources.UnloadAsset(obj);
        }
        _cache.Clear();
    }

    #endregion
}