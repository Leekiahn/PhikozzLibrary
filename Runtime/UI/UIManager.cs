using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    public class UIManager : SingletonGlobal<UIManager>, IUIService, IPreinitialize
    {
        private Dictionary<Type, BaseUIPanel> _panels = new Dictionary<Type, BaseUIPanel>();

        public bool Init()
        {
            try
            {
                ServiceLocator.Register<IUIService>(this);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return false;
            }
        }

        public void RegisterPanel<T>(T panel) where T : BaseUIPanel
        {
            var type = typeof(T);
            if (!_panels.ContainsKey(type))
            {
                _panels[type] = panel;
            }
        }

        public void UnregisterPanel<T>() where T : BaseUIPanel
        {
            var type = typeof(T);
            if (_panels.ContainsKey(type))
            {
                _panels.Remove(type);
            }
        }

        public void ShowPanel<T>() where T : BaseUIPanel
        {
            var type = typeof(T);
            if (_panels.TryGetValue(type, out var panel))
            {
                panel.Open();
            }
        }

        public void HidePanel<T>() where T : BaseUIPanel
        {
            var type = typeof(T);
            if (_panels.TryGetValue(type, out var panel))
            {
                panel.Close();
            }
        }

        public bool IsPanelOpen<T>() where T : BaseUIPanel
        {
            if (_panels.TryGetValue(typeof(T), out var panel))
                return panel.gameObject.activeSelf;
            return false;
        }
    }
}