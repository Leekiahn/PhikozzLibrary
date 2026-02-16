using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PhikozzLibrary;
using UnityEngine;

public class UIManager : SingletonGlobal<UIManager>, IUIService, IPreinitialize
{
    private Dictionary<Enum, GameObject> _panels = new Dictionary<Enum, GameObject>();

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
            Debug.LogWarning("저장 시스템 초기화 실패: " + ex.Message);
            return UniTask.FromResult(false);
        }
    }

    public void RegisterPanel<T>(T key, GameObject panel) where T : Enum
    {
        if (!_panels.ContainsKey(key))
        {
            _panels[key] = panel;
        }
    }
    
    public void UnregisterPanel<T>(T key) where T : Enum
    {
        if (_panels.ContainsKey(key))
        {
            _panels.Remove(key);
        }
    }
    
    public void ShowPanel<T>(T key) where T : Enum
    {
        if (_panels.TryGetValue(key, out var panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"패널을 찾을 수 없습니다: {key}");
        }
    }
    
    public void HidePanel<T>(T key) where T : Enum
    {
        if (_panels.TryGetValue(key, out var panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"패널을 찾을 수 없습니다: {key}");
        }
    }
}