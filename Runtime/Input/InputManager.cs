using Cysharp.Threading.Tasks;
using PhikozzLibrary;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InputManager : SingletonGlobal<InputManager>, IPreinitialize, IInputServiece
{
    private PlayerInputAction _inputActions;
    private InputActionMap _currentActionMap;

    public UniTask<bool> InitAsync()
    {
        _inputActions = new PlayerInputAction();
        _currentActionMap = _inputActions.InGame; // 기본 맵
        ServiceLocator.Register<IInputServiece>(this);
        return UniTask.FromResult(true);
    }

    public void RegisterAction(InputAction action)
    {
        action.Enable();
    }

    public void UnregisterAction(InputAction action)
    {
        action.Disable();
    }

    public void EnableActionMap(string mapName)
    {
        if (_currentActionMap != null)
            _currentActionMap.Disable();

        _currentActionMap = _inputActions.asset.FindActionMap(mapName);
        _currentActionMap?.Enable();
    }

    public void DisableActionMap()
    {
        _currentActionMap?.Disable();
    }
}
