using UnityEngine;
using PhikozzLibrary.Runtime.ServiceLocater;

public class GameManager : GenericSingleton<GameManager>, IGameService
{
    private void Awake()
    {
        base.Awake();
        RegisterServices();
    }
    
    public void RegisterServices()
    {
        ServiceRegister.RegisterAllServices();
    }
}
