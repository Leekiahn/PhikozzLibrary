using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using PhikozzLibrary;
using System;

[Serializable]
public class GameData
{
    // 게임 데이터 변수 추가
}

public class SaveLoadManager : GenericSingleton<SaveLoadManager>
{
    #region >---------------------------------------------- Get Path

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

    public void SaveGame(string saveName)
    {
        GameData data = new GameData
        {
            // 게임 데이터 변수 초기화
        };

        string path = GetPath(saveName);
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
        Debug.Log($"Game saved as: {path}");
    }

    public void LoadGame(string saveName)
    {
        string path = GetPath(saveName);
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file not found");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            GameData data = (GameData)formatter.Deserialize(stream);
            Debug.Log($"Game loaded");
        }
    }

    #endregion
    
    
}