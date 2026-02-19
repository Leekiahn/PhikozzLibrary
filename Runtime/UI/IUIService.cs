using System;
using UnityEngine;

namespace PhikozzLibrary
{
    public interface IUIService
    {
        void RegisterPanel<T>(T panel) where T : BaseUIPanel;
        void UnregisterPanel<T>() where T : BaseUIPanel;
        void ShowPanel<T>() where T : BaseUIPanel;
        void HidePanel<T>() where T : BaseUIPanel;
    }
}