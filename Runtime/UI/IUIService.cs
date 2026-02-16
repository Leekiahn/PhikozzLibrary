using System;
using UnityEngine;

public interface IUIService
{
    void RegisterPanel<T>(T panel) where T : BaseUIPanel;
    void UnregisterPanel<T>() where T : BaseUIPanel;
    void ShowPanel<T>() where T : BaseUIPanel;
    void HidePanel<T>() where T : BaseUIPanel;
}