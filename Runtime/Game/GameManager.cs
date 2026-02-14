using PhikozzLibrary;
using System.Threading.Tasks;

public class GameManager : SingletonGlobal<GameManager>, IGameService
{
    private eGameState currentState;

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