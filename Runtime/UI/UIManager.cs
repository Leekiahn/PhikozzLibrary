using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PhikozzLibrary
{
    public class UIManager : SingletonGlobal<UIManager>, IUIService, IPreinitialize
    {
        private Dictionary<Type, BaseUIPanel> _panels = new Dictionary<Type, BaseUIPanel>();

        public UniTask<bool> InitAsync()
        {
            try
            {
                ServiceLocator.Register<IUIService>(this);
                return UniTask.FromResult(true);
            }
            catch (System.Exception ex)
            {
                // 초기화 실패 처리
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return UniTask.FromResult(false);
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
    }
}