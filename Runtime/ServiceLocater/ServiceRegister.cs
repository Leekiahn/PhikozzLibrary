using System.Collections.Generic;
using UnityEngine;
using PhikozzLibrary;

public class ServiceRegister : MonoBehaviour
{
    public List<GameObject> managerPrefabs = new List<GameObject>(); // 매니저 프리팹 리스트

    private void Awake()
    {
        InstantiateManagers();
        RegisterAllServices();
    }

    private void RegisterAllServices()
    {
        ServiceLocator.Register<IGameService>(GameManager.Instance);
        ServiceLocator.Register<IAudioService>(AudioManager.Instance);
        ServiceLocator.Register<IPoolService>(PoolManager.Instance);
        ServiceLocator.Register<IEventService>(EventManager.Instance);
        // 서비스 등록 추가
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