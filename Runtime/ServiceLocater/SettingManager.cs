using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using PhikozzLibrary;

[SerializeField]
public class SettingData
{
    // 설정 데이터 변수 추가
    public float masterVolume;

    /// <summary>
    /// 기본값 설정
    /// </summary>
    public SettingData()
    {
        masterVolume = 1.0f;
    }
}

public class SettingManager : GenericSingleton<SettingManager>
{
    #region >---------------------------------------------- Fields

    public SettingData settingData = new SettingData();
    private readonly BinaryFormatter _formatter = new BinaryFormatter();

    #endregion

    #region >---------------------------------------------- Get Path

    
    /// <summary>
    /// 저장 파일 경로 반환 (SaveManager와 동일한 구조)
    /// </summary>
    private string GetPath(string saveName)
    {
        string folder = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
            ".PhikozzLibrarySaves"
        );
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        return Path.Combine(folder, saveName + ".bin");
    }

    #endregion

    #region >---------------------------------------------- Save & Load

    
    /// <summary>
    /// 설정을 저장
    /// </summary>
    public void SaveSetting(string saveName = "SettingData")
    {
        string path = GetPath(saveName);
        using (var fs = new FileStream(path, FileMode.Create))
        {
            _formatter.Serialize(fs, settingData);
        }
        Debug.Log($"Settings saved as: {path}");
    }

    /// <summary>
    /// 설정을 불러오기
    /// </summary>
    public void LoadSetting(string saveName = "SettingData")
    {
        string path = GetPath(saveName);
        if (!File.Exists(path))
        {
            Debug.LogWarning("Setting file not found");
            return;
        }
        using (var fs = new FileStream(path, FileMode.Open))
        {
            var data = (SettingData)_formatter.Deserialize(fs);
            settingData = data;
        }
        Debug.Log("Settings loaded");
    }

    #endregion
}