## Global.Game - GameMananger
- GameManager는 게임의 상태(진행, 일시정지, 종료 등)를 관리하는 싱글턴 매니저 클래스입니다.  
  게임 상태 변경 시 이벤트를 통해 다른 시스템과 연동할 수 있습니다.

### eGameState (enum)
- None: 초기 상태
- Playing: 게임 진행 중
- Paused: 일시정지 상태
- GameOver: 게임 종료

### 주요 메서드
- PauseGame(): 게임을 일시정지 상태로 전환하고, Time.timeScale을 0으로 설정합니다.
- ResumeGame(): 게임을 진행 상태로 전환하고, Time.timeScale을 1로 복구합니다.
- EndGame(): 게임을 종료 상태로 전환합니다.
- RestartGame(): 게임을 재시작하며, 상태를 Playing으로 변경합니다.
<br />

## Global.Scene - GameSceneManager
- GameSceneManager는 Unity에서 게임 씬의 전환 및 관리를 담당하는 싱글턴 매니저 클래스입니다.  
씬 로드, 현재 씬 리로드, 씬 로드 후 처리 기능을 제공합니다.

### 주요 메서드
- LoadScene(string sceneName): 지정한 이름의 씬을 로드합니다.
- ReloadCurrentScene(): 현재 활성화된 씬을 다시 로드합니다.
- OnSceneLoaded(Scene scene, LoadSceneMode mode): 씬이 로드된 후 실행할 작업을 정의할 수 있습니다.  
(예: 초기화, 플레이어 위치 설정 등)
