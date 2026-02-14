using System.Collections.Generic;
using UnityEngine;
using PhikozzLibrary;

public class ServiceRegister : MonoBehaviour
{
    public List<GameObject> managerPrefabs = new List<GameObject>(); // 매니저 프리팹 리스트

    private async void Awake()
    {
        InstantiateManagers();
        
        await GameManager.Instance.InitAsync();
        await AudioManager.Instance.InitAsync();
        await PoolManager.Instance.InitAsync();
        await EventManager.Instance.InitAsync();
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