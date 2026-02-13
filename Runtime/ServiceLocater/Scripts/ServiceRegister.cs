using UnityEngine;
using PhikozzLibrary.Runtime.ServiceLocater;

public static class ServiceRegister
{
    public static void RegisterAllServices()
    {
        ServiceLocator.Register<IAudioService>(AudioManager.Instance);
        ServiceLocator.Register<IGameService>(GameManager.Instance);
        // 서비스 등록 추가
    }
}
