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
<br />

## Global.SaveLoad - SaveLoadManager
- SaveLoadManager는 게임 데이터를 저장하고 불러오는 기능을 제공하는 싱글턴 매니저 클래스입니다.  
로컬 폴더(MyDocuments/.PhikozzLibrarySaves)에 바이너리 형식으로 데이터를 저장/로드합니다.

### GameData (직렬화 클래스)
- 저장할 게임 데이터 구조를 정의합니다.
- 필요한 변수들을 추가하여 사용합니다.

### 주요 메서드
- SaveGame(string saveName): 현재 게임 데이터를 바이너리 파일로 저장합니다. 저장 경로는 MyDocuments/.PhikozzLibrarySaves/ 하위에 생성됩니다.
- LoadGame(string saveName): 지정한 이름의 저장 파일을 불러와 게임 데이터를 복원합니다. 파일이 없을 경우 경고 메시지를 출력합니다.
- GetPath(string saveName): 저장 파일의 전체 경로를 반환합니다. 폴더가 없으면 자동으로 생성합니다.
<br />
