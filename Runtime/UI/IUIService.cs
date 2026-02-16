using System;
using UnityEngine;

public interface IUIService
{
    void RegisterPanel<T>(T key, GameObject panel) where T : Enum;
    void UnregisterPanel<T>(T key) where T : Enum;
    void ShowPanel<T>(T key) where T : Enum;
    void HidePanel<T>(T key) where T : Enum;
}