using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using PhikozzLibrary;

public class PoolManager : SingletonGlobal<PoolManager>, IPoolService, IPreinitialize
{
    private Dictionary<string, object> _pools = new Dictionary<string, object>();
    private Dictionary<string, HashSet<MonoBehaviour>>
        _activeObjects = new Dictionary<string, HashSet<MonoBehaviour>>();

    public UniTask<bool> InitAsync()
    {
        try
        {
            ServiceLocator.Register<IPoolService>(this);
            return UniTask.FromResult(true);
        }
        catch (System.Exception ex)
        {
            // 초기화 실패 처리
            Debug.LogWarning("저장 시스템 초기화 실패: " + ex.Message);
            return UniTask.FromResult(false);
        }
    }

    public void CreatePool<T>(T prefab, int defaultCapacity, int maxSize) where T : MonoBehaviour, IPoolObject
    {
        if (!_pools.ContainsKey(prefab.name))
        {
            var pool = new ObjectPool<T>(
                createFunc: () =>
                {
                    var obj = Instantiate(prefab);
                    obj.OnCreate();
                    return obj;
                },
                actionOnGet: obj =>
                {
                    obj.gameObject.SetActive(true);
                    obj.OnGet();
                    if (!_activeObjects.ContainsKey(prefab.name))
                        _activeObjects[prefab.name] = new HashSet<MonoBehaviour>();
                    _activeObjects[prefab.name].Add(obj);
                },
                actionOnRelease: obj =>
                {
                    obj.gameObject.SetActive(false);
                    obj.OnRelease();
                    _activeObjects[prefab.name].Remove(obj);
                },
                actionOnDestroy: obj =>
                {
                    if (obj != null)
                    {
                        obj.OnDestroy();
                        Destroy(obj.gameObject);
                    }
                },
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            );
            _pools.Add(prefab.name, pool);
        }
    }

    public ObjectPool<T> Get<T>(T prefab) where T : MonoBehaviour, IPoolObject
    {
        if (_pools.TryGetValue(prefab.name, out var pool))
        {
            return pool as ObjectPool<T>;
        }
        else
        {
            Debug.LogError($"해당 프리팹에 대한 풀을 찾을 수 없습니다: {prefab.name}. 먼저 풀을 생성해주세요.");
            return null;
        }
    }

    public void Release<T>(T prefab) where T : MonoBehaviour, IPoolObject
    {
        if (_activeObjects.TryGetValue(prefab.name, out var set) && set.Count > 0)
        {
            var obj = set.FirstOrDefault() as T;
            if (obj != null)
                Get(prefab).Release(obj);
        }
        else
        {
            Debug.LogWarning($"{prefab.name}에 대한 활성화된 객체가 없습니다. 풀에서 객체를 가져와서 사용한 후에 릴리스해주세요.");
        }
    }

    public void ReleaseAll<T>(T prefab) where T : MonoBehaviour, IPoolObject
    {
        if (_activeObjects.TryGetValue(prefab.name, out var set))
        {
            var copy = set.ToList();
            foreach (var obj in copy)
            {
                Get(prefab).Release(obj as T);
            }

            set.Clear();
        }
    }

    public void DestroyAll<T>(T prefab) where T : MonoBehaviour, IPoolObject
    {
        if (_pools.TryGetValue(prefab.name, out var pool))
        {
            var objectPool = pool as ObjectPool<T>;
            objectPool.Clear();
            _pools.Remove(prefab.name);
        }

        if (_activeObjects.TryGetValue(prefab.name, out var set))
        {
            foreach (var obj in set)
            {
                if (obj != null)
                {
                    (obj as T).OnDestroy();
                    Destroy(obj.gameObject);
                }
            }

            set.Clear();
            _activeObjects.Remove(prefab.name);
        }
    }

    public ObjectPool<T> GetPool<T>(T prefab) where T : MonoBehaviour, IPoolObject
    {
        if (_pools.TryGetValue(prefab.name, out var pool))
        {
            return pool as ObjectPool<T>;
        }
        else
        {
            Debug.LogError($"해당 프리팹에 대한 풀을 찾을 수 없습니다: {prefab.name}. 먼저 풀을 생성해주세요.");
            return null;
        }
    }
}