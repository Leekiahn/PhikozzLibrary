using UnityEngine;
using System;

namespace PhikozzLibrary
{
    public class GameManager : SingletonGlobal<GameManager>, IGameService, IInitializable
    {
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
    }
}