using UnityEngine;
using PhikozzLibrary;

public class ServiceRegister : MonoBehaviour
{
    private void Awake()
    {
        RegisterAllServices();
    }
    
    private void RegisterAllServices()
    {
        ServiceLocator.Register<IAudioService>(AudioManager.Instance);
        ServiceLocator.Register<IGameService>(GameManager.Instance);
        // 서비스 등록 추가
    }
}
