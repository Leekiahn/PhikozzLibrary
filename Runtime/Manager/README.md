# Global

## Global.Game - GameMananger
- GameManager는 게임의 상태(진행, 일시정지, 종료 등)를 관리하는 싱글턴 매니저 클래스입니다.  
  게임 상태 변경 시 이벤트를 통해 다른 시스템과 연동할 수 있습니다.

### eGameState (enum)
- None: 초기 상태
- Playing: 게임 진행 중
- Paused: 일시정지 상태
- GameOver: 게임 종료

### 주요 기능
- PauseGame(): 게임을 일시정지 상태로 전환하고, Time.timeScale을 0으로 설정합니다.
- ResumeGame(): 게임을 진행 상태로 전환하고, Time.timeScale을 1로 복구합니다.
- EndGame(): 게임을 종료 상태로 전환합니다.
- RestartGame(): 게임을 재시작하며, 상태를 Playing으로 변경합니다.
