using System.Collections.Generic;
using UnityEngine;
using PhikozzLibrary;

public class ServiceBootstrapper : MonoBehaviour
{
    public List<GameObject> managerPrefabs = new List<GameObject>(); // 매니저 프리팹 리스트

    private async void Awake()
    {
        InstantiateManagers();
        string initResult = string.Empty;
        
        initResult = await GameManager.Instance.InitAsync() ? "✅ GameManager 초기화 성공" : "❌ GameManager 초기화 실패";
        Debug.Log(initResult);
        initResult = await ResourceManager.Instance.InitAsync() ? "✅ ResourceManager 초기화 성공" : "❌ ResourceManager 초기화 실패";
        Debug.Log(initResult);
        initResult = await AudioManager.Instance.InitAsync() ? "✅ AudioManager 초기화 성공" : "❌ AudioManager 초기화 실패";
        Debug.Log(initResult);
        initResult = await PoolManager.Instance.InitAsync() ? "✅ PoolManager 초기화 성공" : "❌ PoolManager 초기화 실패";
        Debug.Log(initResult);
        initResult = await EventManager.Instance.InitAsync() ? "✅ EventManager 초기화 성공" : "❌ EventManager 초기화 실패";
        Debug.Log(initResult);
    }

    private void InstantiateManagers()
    {
        foreach (var prefab in managerPrefabs)
        {
            if (prefab != null)
            {
                Instantiate(prefab);
            }
        }
    }
}