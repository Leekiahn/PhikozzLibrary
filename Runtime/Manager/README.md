## # Global.Game - GameManager
- **GameManager**는 게임의 상태(진행, 일시정지, 종료 등)를 관리하는 싱글턴 매니저 클래스입니다.  
게임 상태 변경 시 이벤트를 통해 다른 시스템과 연동할 수 있습니다.

### ## eGameState (enum)
- None: 초기 상태  
- Playing: 게임 진행 중  
- Paused: 일시정지 상태  
- GameOver: 게임 종료  

### ## 주요 메서드
- **PauseGame()**: 게임을 일시정지하며, 일시정지 상태로 전환하고 `Time.timeScale`을 0으로 설정합니다.
- **ResumeGame()**: 게임을 재개하며, 진행 상태로 전환하고 `Time.timeScale`을 1로 복구합니다.
- **EndGame()**: 게임을 종료 상태로 전환합니다.
- **RestartGame()**: 게임을 재시작하며, 상태를 `Playing`으로 변경합니다.

<br />

## # Global.Event - EventManager

- **EventManager**는 전역적으로 커스텀 이벤트를 등록, 호출, 관리할 수 있는 싱글턴 매니저 클래스입니다.
<br />

## # Global.Scene - GameSceneManager
- **GameSceneManager**는 Unity에서 게임 씬의 전환 및 관리를 담당하는 싱글턴 매니저 클래스입니다.  
씬 로드, 현재 씬 리로드, 씬 로드 후 처리 기능을 제공합니다.

### ## 주요 메서드
- **LoadScene(string sceneName)**: 지정한 이름의 씬을 로드합니다.
- **ReloadCurrentScene()**: 현재 활성화된 씬을 다시 로드합니다.
- **OnSceneLoaded(Scene scene, LoadSceneMode mode)**: 씬이 로드된 후 실행할 작업을 정의할 수 있습니다.  
  (예: 초기화, 플레이어 위치 설정 등)

<br />

## # Global.SaveLoad - SaveLoadManager
- **SaveLoadManager**는 게임 데이터를 저장하고 불러오는 기능을 제공하는 싱글턴 매니저 클래스입니다.  
로컬 폴더(`MyDocuments/.PhikozzLibrarySaves`)에 바이너리 형식으로 데이터를 저장/로드합니다.

### ## GameData (직렬화 클래스)
- 저장할 게임 데이터 구조를 정의합니다.  
- 필요한 변수를 추가하여 사용합니다.

### ## 주요 메서드
- **SaveGame(string saveName)**: 현재 게임 데이터를 바이너리 파일로 저장합니다. 저장 경로는 `MyDocuments/.PhikozzLibrarySaves/` 하위에 생성됩니다.
- **LoadGame(string saveName)**: 지정한 이름의 저장 파일을 불러와 게임 데이터를 복원합니다. 파일이 없을 경우 경고 메시지를 출력합니다.
- **GetPath(string saveName)**: 저장 파일의 전체 경로를 반환합니다. 폴더가 없으면 자동으로 생성합니다.

<br />

## # Global.Input - InputManager
- **InputManager**는 Unity Input System 기반의 입력을 관리하는 싱글턴 매니저 클래스입니다.  
플레이어 입력 및 UI 입력을 처리하며, 입력 이벤트와 지속 입력 값을 제공합니다.

### ## 주요 필드 및 프로퍼티
- **moveInput**: 현재 플레이어의 이동 입력값(`Vector2`). 지속적으로 갱신됩니다.
- **OnESCAction**: ESC 키 입력 시 발생하는 이벤트입니다.

### ## 주요 메서드
- **SetActionMap(string actionMapName)**: 액션맵(`Player`, `UI` 등)을 전환하여 입력 모드를 변경합니다.
- **OnMovePerformed(InputAction.CallbackContext context)**: 이동 입력이 발생할 때 호출되어 `moveInput` 값을 갱신합니다.
- **OnMoveCanceled(InputAction.CallbackContext context)**: 이동 입력이 해제될 때 호출되어 `moveInput`을 초기화합니다.
- **OnESCStarted(InputAction.CallbackContext context)**: ESC 키가 눌렸을 때 `OnESCAction` 이벤트를 발생시킵니다.

<br />

## # Global.UI - UIManager
- **UIManager**는 Unity에서 UI 패널의 표시/숨김 및 UI 사운드 재생을 관리하는 싱글턴 매니저 클래스입니다.  
패널을 이름으로 관리하며, 오디오 소스를 통해 UI 사운드를 재생할 수 있습니다.

### ## 주요 필드 및 프로퍼티
- **_uiPanels**: 에디터에서 등록한 UI 패널 리스트입니다.
- **_panelDict**: 패널 이름과 GameObject를 매핑하는 딕셔너리입니다.

### ## 주요 메서드
- **ShowPanel(string panelName)**: 지정한 이름의 패널을 표시합니다.
- **ShowOnlyPanel(string panelName)**: 지정한 패널만 표시하고 나머지는 모두 숨깁니다.
- **HidePanel(string panelName)**: 지정한 이름의 패널을 숨깁니다.
- **HideAllPanels()**: 모든 패널을 숨깁니다.
- **PlayUISound(AudioClip clip, float volume = 1.0f)**: 지정한 오디오 클립을 UI 사운드로 재생합니다.

<br />

## # Global.BGM - BGMManager
- **BGMManager**는 길이가 긴 배경음악(BGM) 재생을 관리하는 싱글턴 매니저 클래스입니다.  
오디오 소스를 통해 BGM을 재생 및 정지할 수 있습니다.

### ## 주요 필드 및 프로퍼티
- **_audioSource**: BGM 재생에 사용되는 AudioSource 컴포넌트입니다.

### ## 주요 메서드
- **PlayBGM(AudioClip clip, float volume = 1.0f)**: 지정한 오디오 클립을 주어진 볼륨으로 BGM으로 재생합니다.
- **StopBGM()**: 현재 재생 중인 BGM을 정지합니다.

<br />

## # Global.SFX - SFXManager
- **SFXManager**는 효과음(SFX) 재생을 관리하는 싱글턴 매니저 클래스입니다.  
오디오 소스를 통해 효과음을 재생할 수 있습니다.

### ## 주요 필드 및 프로퍼티
- **_audioSource**: SFX 재생에 사용되는 AudioSource 컴포넌트입니다.

### ## 주요 메서드
- **PlaySFX(AudioClip clip, float volume = 1.0f)**: 지정한 오디오 클립을 효과음으로 재생합니다.

<br />

## # Global.Resource - ResourceManager
- **ResourceManager**는 Unity의 Resources 폴더 내 리소스(프리팹, 오디오, 텍스처 등)를 효율적으로 로드/언로드/캐싱하는 싱글턴 매니저 클래스입니다.

### ## 주요 필드 및 프로퍼티
- **_cache**: 단일 오브젝트 캐시 딕셔너리
- **_cacheAll**: 다중 오브젝트 캐시 딕셔너리

### ## 주요 메서드
- **Load<T>(string path)**: 지정한 경로의 리소스를 로드하고 캐싱합니다.
- **LoadAll<T>(string path)**: 지정한 경로의 모든 리소스를 배열로 로드하고 캐싱합니다.
- **Unload(string path)**: 지정한 경로의 리소스를 언로드합니다.
- **UnloadAll()**: 모든 캐시된 리소스를 언로드합니다.
<br />

## # Global.Timer - TimerManager

- **TimerManager**는 Unity에서 타이머 기반 작업을 쉽게 관리할 수 있도록 도와주는 싱글턴 매니저 클래스입니다.  
  타이머 시작, 반복, 중단(즉시/지연) 기능을 제공합니다.

### ## 주요 메서드
- **StartTimer(float delay, Action callback, bool repeat = false)**  
  지정한 시간 후 콜백을 실행하는 타이머를 시작합니다. 반복 실행도 지원합니다.

- **StopTimer(Coroutine timerCoroutine, float delayAfterStop)**  
  진행 중인 타이머 코루틴을 즉시 또는 일정 시간 후 중단합니다.

### ## 예시

```csharp
// 2초 후 한 번 실행
Coroutine timer = TimerManager.Instance.StartTimer(2f, () => {
    Debug.Log("2초 후 실행!");
});

// 1초마다 반복 실행
Coroutine repeatTimer = TimerManager.Instance.StartTimer(1f, () => Debug.Log("Tick"), true);

// 5초 후 반복 타이머 중단
TimerManager.Instance.StopTimer(repeatTimer, 5f);
```
- StopTimer의 delayAfterStop을 0f로 주면 즉시 중단과 동일합니다.
- 반복 타이머는 반드시 외부에서 중단해주어야 합니다.
  
