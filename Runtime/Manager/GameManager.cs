using System;
using UnityEngine;
using PhikozzLibrary;

public enum eGameState
{
    None,
    Playing,
    Paused,
    GameOver
}

public class GameManager : GenericSingleton<GameManager>
{
    #region >--------------------------------------------- fields & Properties

    private static eGameState _state = eGameState.None;    // 현재 게임 상태
    public static event Action<eGameState> OnGameStateChanged; // 게임 상태 변경 이벤트

    #endregion

    #region >--------------------------------------------- Set

    /// <summary>
    /// 게임 상태 설정
    /// </summary>
    /// <param name="newState">게임 상태 열거형</param>
    private static void SetGameState(eGameState newState)
    {
        if (_state == newState) return;
        _state = newState;
        OnGameStateChanged?.Invoke(_state);
    }

    #endregion

    #region >--------------------------------------------- Core

    /// <summary>
    /// 일시정지, 재개, 종료, 재시작 기능
    /// </summary>
    public static void PauseGame()
    {
        // 일시정지 로직
        SetGameState(eGameState.Paused);
        Time.timeScale = 0f; // 게임 시간 정지
    }

    public static void ResumeGame()
    {
        // 재개 로직
        SetGameState(eGameState.Playing);
        Time.timeScale = 1f; // 게임 시간 재개
    }

    public static void EndGame()
    {
        // 게임 종료 로직
        SetGameState(eGameState.GameOver);
    }

    public static void RestartGame()
    {
        // 게임 재시작 로직
        SetGameState(eGameState.Playing);
    }

    #endregion
}