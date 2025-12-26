using System;
using System.Collections.Generic;
using PhikozzLibrary;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
    #region >--------------------------------------------- fields & Properties

    [Serializable]
    public class PanelInfo
    {
        public string panelName;
        public GameObject panelObject;
    }
    
    [SerializeField] private List<PanelInfo> _uiPanels = new List<PanelInfo>();
    private Dictionary<string, GameObject> _panelDict = new Dictionary<string, GameObject>();
    
    private AudioSource _audioSource;

    #endregion

    #region >--------------------------------------------- Unity

    protected override void Awake()
    {
        base.Awake();
        _audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    private void Start()
    {
        SetCanvasTabs(_uiPanels);
    }

    #endregion

    #region >--------------------------------------------- Set

    private void SetPanel(string panelName, GameObject panel)
    {
        if (!_panelDict.TryAdd(panelName, panel))
        {
            _panelDict.Add(panelName, panel);
        }
    }
    
    private void SetCanvasTabs(List<PanelInfo> tabs)
    {
        foreach (var tab in tabs)
        {
            SetPanel(tab.panelName, tab.panelObject);
        }
    }

    #endregion

    #region >--------------------------------------------- Show & Hide

    public void ShowPanel(string panelName)
    {
        if (_panelDict.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found.");
        }
    }
    
    public void ShowOnlyPanel(string panelName)
    {
        foreach (var kvp in _panelDict)
        {
            kvp.Value.SetActive(kvp.Key == panelName);
        }
    }
    
    public void HidePanel(string panelName)
    {
        if (_panelDict.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found.");
        }
    }

    public void HideAllPanels()
    {
        foreach (var panel in _panelDict.Values)
        {
            panel.SetActive(false);
        }
    }
    
    public void PlayUISound(AudioClip clip, float volume = 1.0f)
    {
        if(_audioSource == null) return;
        _audioSource.PlayOneShot(clip, volume);
    }

    #endregion
}
