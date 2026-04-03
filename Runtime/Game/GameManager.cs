using UnityEngine;
using System;

namespace PhikozzLibrary
{
    public class GameManager : SingletonGlobal<GameManager>, IGameService, IInitializable
    {
        private eGameState currentState;

        public bool Init()
        {
            try
            {
                ServiceLocator.Register<IGameService>(this);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return false;
            }
        }


        public void StartGame()
        {
            currentState = eGameState.Playing;
            // 추가적인 게임 시작 로직
        }

        public void PauseGame()
        {
            if (currentState == eGameState.Playing)
            {
                currentState = eGameState.Paused;
                // 추가적인 게임 일시정지 로직
            }
        }

        public void ResumeGame()
        {
            if (currentState == eGameState.Paused)
            {
                currentState = eGameState.Playing;
                // 추가적인 게임 재개 로직
            }
        }

        public void EndGame()
        {
            currentState = eGameState.Ended;
            // 추가적인 게임 종료 로직
        }
    }
}