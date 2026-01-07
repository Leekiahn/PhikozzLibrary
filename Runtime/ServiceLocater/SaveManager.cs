using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using PhikozzLibrary;
using System;

/// <summary>
/// 게임 데이터를 저장하는 클래스
/// </summary>
[Serializable]
public class GameData
{
    // 게임 데이터 변수 추가
    public int playerLevel;

    /// <summary>
    /// 기본값 설정
    /// </summary>
    public GameData()
    {
        playerLevel = 1;
    }
}

public class SaveManager : GenericSingleton<SaveManager>
{
    #region >---------------------------------------------- Fields

    public GameData gameData = new GameData();  // 현재 게임 데이터를 저장하는 객체
    private readonly BinaryFormatter _formatter = new BinaryFormatter();
    private FileStream _fileStream = null;

    #endregion

    #region >---------------------------------------------- Get Path

    /// <summary>
    /// 저장 파일의 전체 경로를 반환
    /// </summary>
    /// <param name="saveName">세이브 이름</param>
    /// <returns>전체 경로</returns>
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
    /// 게임 데이터를 저장
    /// </summary>
    /// <param name="saveName">세이브 이름</param>
    public void SaveGame(string saveName)
    {
        // 필요한 데이터로 GameData 객체를 채움
        GameData data = new GameData
        {
            // 예: playerLevel = gameData.playerLevel,
            playerLevel = gameData.playerLevel,
        };

        string path = GetPath(saveName);
        using (_fileStream = new FileStream(path, FileMode.Create))   // 파일 스트림 생성
        {
            _formatter.Serialize(_fileStream, data);  // 데이터 직렬화 및 저장
        }
        Debug.Log($"Game saved as: {path}");
    }

    /// <summary>
    /// 게임 데이터를 불러옴
    /// </summary>
    /// <param name="saveName">세이브 이름</param>
    public void LoadGame(string saveName)
    {
        string path = GetPath(saveName);
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file not found");
            return;
        }

        using (_fileStream = new FileStream(path, FileMode.Open)) // 파일 스트림 생성
        {
            GameData data = (GameData)_formatter.Deserialize(_fileStream);    // 데이터 역직렬화
            gameData = data;    // 불러온 데이터로 현재 게임 데이터 갱신
            Debug.Log($"Game loaded");
        }
    }

    #endregion
    
    
}