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
    public T[] LoadAll<T>(string path) where T : Object
    {
        path = NormalizeResourcePath(path);
        var assets = Resources.LoadAll<T>(path);
        foreach (var asset in assets)
        {
            if (asset != null)
            {
                var assetPath = $"{path}/{asset.name}";
                if (!_cache.ContainsKey(assetPath))
                    _cache[assetPath] = asset;
            }
        }
        return assets;
    }

    #endregion

    #region >--------------------------------------------- Unload

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