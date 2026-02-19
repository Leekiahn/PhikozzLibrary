using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputServiece
{
    void RegisterAction(InputAction action);
    void UnregisterAction(InputAction action);
    void EnableActionMap();
    void DisableActionMap();
}