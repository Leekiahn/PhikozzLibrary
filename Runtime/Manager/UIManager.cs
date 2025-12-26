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
    
    private static Dictionary<string, GameObject> _panelDict = new Dictionary<string, GameObject>();
    private AudioSource _audioSource;
    
    public static readonly string PAUSE_PANEL = "Pause";
    public static readonly string GAMEOVER_PANEL = "GameOver";

    #endregion

    #region >--------------------------------------------- Unity

    protected override void Awake()
    {
        base.Awake();
        _audioSource = gameObject.AddComponent<AudioSource>();
        SetCanvasTabs(_uiPanels);
    }

    private void OnEnable()
    {
    }
    
    private void OnDisable()
    {
    }


    #endregion

    #region >--------------------------------------------- Set

    /// <summary>
    /// Canvas 탭 세팅
    /// </summary>
    /// <param name="tabs">직렬화된 탭 리스트</param>
    private void SetCanvasTabs(List<PanelInfo> tabs)
    {
        foreach (var tab in tabs)
        {
            SetPanel(tab.panelName, tab.panelObject);
        }
    }
    
    /// <summary>
    /// 딕셔너리에 패널 추가
    /// </summary>
    /// <param name="panelName">패널 이름 key</param>
    /// <param name="panel">패널 게임오브젝트 value</param>
    private void SetPanel(string panelName, GameObject panel)
    {
        if (!_panelDict.ContainsKey(panelName))
        {
            _panelDict.Add(panelName, panel);
        }
        else
        {
            _panelDict[panelName] = panel; // 이미 있으면 덮어쓰기
        }
    }

    #endregion

    #region >--------------------------------------------- Show & Hide

    /// <summary>
    /// 패널 보이기
    /// </summary>
    /// <param name="panelName">패널 이름 key</param>
    public static void ShowPanel(string panelName)
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
    
    /// <summary>
    /// 패널 하나만 보이기
    /// </summary>
    /// <param name="panelName">패널 이름 Key</param>
    public static void ShowOnlyPanel(string panelName)
    {
        foreach (var kvp in _panelDict)
        {
            kvp.Value.SetActive(kvp.Key == panelName);
        }
    }
    
    /// <summary>
    /// 패널 숨기기
    /// </summary>
    /// <param name="panelName">패널 이름 Key</param>
    public static void HidePanel(string panelName)
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

    /// <summary>
    /// 패널 모두 숨기기
    /// </summary>
    public static void HideAllPanels()
    {
        foreach (var panel in _panelDict.Values)
        {
            panel.SetActive(false);
        }
    }
    


    #endregion
    
    #region >--------------------------------------------- Core
    
    /// <summary>
    /// UI 사운드 재생
    /// </summary>
    /// <param name="clip">리소스 매니저를 통해 불러온 AudioClip</param>
    /// <param name="volume">볼륨</param>
    public void PlayUISound(AudioClip clip, float volume = 1.0f)
    {
        if(_audioSource == null) return;
        _audioSource.PlayOneShot(clip, volume);
    }
    
    #endregion
}
