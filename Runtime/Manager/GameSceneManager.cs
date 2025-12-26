using PhikozzLibrary;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임 씬 매니저
/// </summary>
public class GameSceneManager : GenericSingleton<GameSceneManager>
{
    #region >--------------------------------------------- Load

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
    public void ReloadCurrentScene()
    {
        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드된 후 실행할 코드 작성
        // 예: 초기화 작업, 플레이어 위치 설정 등
    }

    #endregion
}
