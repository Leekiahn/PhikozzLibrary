## 목차
- [GameManager](#globalgame---gamemanager)
- [EventManager](#globalevent---eventmanager)
- [GameSceneManager](#globalscene---gamescenemanager)
- [SaveLoadManager](#globalsaveload---saveloadmanager)
- [InputManager](#globalinput---inputmanager)
- [UIManager](#globalui---uimanager)
- [BGMManager](#globalbgm---bgmmanager)
- [SFXManager](#globalsfx---sfxmanager)
- [ResourceManager](#globalresource---resourcemanager)
- [TimerManager](#globaltimer---timermanager)

---

## Global.Game - GameManager
- **GameManager**는 게임의 상태(진행, 일시정지, 종료 등)를 관리하는 싱글턴 매니저 클래스입니다.  
  게임 상태 변경 시 이벤트를 통해 다른 시스템과 연동할 수 있습니다.

### eGameState (enum)
- None: 초기 상태  
- Playing: 게임 진행 중  
- Paused: 일시정지 상태  
- GameOver: 게임 종료  

### 주요 메서드
- **PauseGame()**: 게임을 일시정지하며, 일시정지 상태로 전환하고 `Time.timeScale`을 0으로 설정합니다.
- **ResumeGame()**: 게임을 재개하며, 진행 상태로 전환하고 `Time.timeScale`을 1로 복구합니다.
- **EndGame()**: 게임을 종료 상태로 전환합니다.
- **RestartGame()**: 게임을 재시작하며, 상태를 `Playing`으로 변경합니다.

---

## Global.Event - EventManager
- **EventManager**는 전역적으로 커스텀 이벤트를 등록, 호출, 관리할 수 있는 싱글턴 매니저 클래스입니다.

---

## Global.Scene - GameSceneManager
- **GameSceneManager**는 Unity에서 게임 씬의 전환 및 관리를 담당하는 싱글턴 매니저 클래스입니다.  
  씬 로드, 현재 씬 리로드, 씬 로드 후 처리 기능을 제공합니다.

### 주요 메서드
- **LoadScene(string sceneName)**: 지정한 이름의 씬을 로드합니다.
- **ReloadCurrentScene()**: 현재 활성화된 씬을 다시 로드합니다.
- **OnSceneLoaded(Scene scene, LoadSceneMode mode)**: 씬이 로드된 후 실행할 작업을 정의할 수 있습니다. (예: 초기화, 플레이어 위치 설정 등)

---

## Global.SaveLoad - SaveLoadManager
- **SaveLoadManager**는 게임 데이터를 저장하고 불러오는 기능을 제공하는 싱글턴 매니저 클래스입니다.  
  로컬 폴더(`MyDocuments/.PhikozzLibrarySaves`)에 바이너리 형식으로 데이터를 저장/로드합니다.

### GameData (직렬화 클래스)
- 저장할 게임 데이터 구조를 정의합니다.  
- 필요한 변수를 추가하여 사용합니다.

### 주요 메서드
- **SaveGame(string saveName)**: 현재 게임 데이터를 바이너리 파일로 저장합니다. 저장 경로는 `MyDocuments/.PhikozzLibrarySaves/` 하위에 생성됩니다.
- **LoadGame(string saveName)**: 지정한 이름의 저장 파일을 불러와 게임 데이터를 복원합니다. 파일이 없을 경우 경고 메시지를 출력합니다.
- **GetPath(string saveName)**: 저장 파일의 전체 경로를 반환합니다. 폴더가 없으면 자동으로 생성합니다.

---

## Global.Input - InputManager
- **InputManager**는 Unity Input System 기반의 입력을 관리하는 싱글턴 매니저 클래스입니다.  
  플레이어 입력 및 UI 입력을 처리하며, 입력 이벤트와 지속 입력 값을 제공합니다.

### 주요 필드 및 프로퍼티
- **moveInput**: 현재 플레이어의 이동 입력값(`Vector2`). 지속적으로 갱신됩니다.
- **OnESCAction**: ESC 키 입력 시 발생하는 이벤트입니다.

### 주요 메서드
- **SetActionMap(string actionMapName)**: 액션맵(`Player`, `UI` 등)을 전환하여 입력 모드를 변경합니다.
- **OnMovePerformed(InputAction.CallbackContext context)**: 이동 입력이 발생할 때 호출되어 `moveInput` 값을 갱신합니다.
- **OnMoveCanceled(InputAction.CallbackContext context)**: 이동 입력이 해제될 때 호출되어 `moveInput`을 초기화합니다.
- **OnESCStarted(InputAction.CallbackContext context)**: ESC 키가 눌렸을 때 `OnESCAction` 이벤트를 발생시킵니다.

---

## Global.UI - UIManager
- **UIManager**는 Unity에서 UI 패널의 표시/숨김 및 UI 사운드 재생을 관리하는 싱글턴 매니저 클래스입니다.  
  패널을 이름으로 관리하며, 오디오 소스를 통해 UI 사운드를 재생할 수 있습니다.

### 주요 필드 및 프로퍼티
- **_uiPanels**: 에디터에서 등록한 UI 패널 리스트입니다.
- **_panelDict**: 패널 이름과 GameObject를 매핑하는 딕셔너리입니다.

### 주요 메서드
- **ShowPanel(string panelName)**: 지정한 이름의 패널을 표시합니다.
- **ShowOnlyPanel(string panelName)**: 지정한 패널만 표시하고 나머지는 모두 숨깁니다.
- **HidePanel(string panelName)**: 지정한 이름의 패널을 숨깁니다.
- **HideAllPanels()**: 모든 패널을 숨깁니다.
- **PlayUISound(AudioClip clip, float volume = 1.0f)**: 지정한 오디오 클립을 UI 사운드로 재생합니다.

---

## Global.BGM - BGMManager
- **BGMManager**는 길이가 긴 배경음악(BGM) 재생을 관리하는 싱글턴 매니저 클래스입니다.  
  오디오 소스를 통해 BGM을 재생 및 정지할 수 있습니다.

### 주요 필드 및 프로퍼티
- **_audioSource**: BGM 재생에 사용되는 AudioSource 컴포넌트입니다.

### 주요 메서드
- **PlayBGM(AudioClip clip, float volume = 1.0f)**: 지정한 오디오 클립을 주어진 볼륨으로 BGM으로 재생합니다.
- **StopBGM()**: 현재 재생 중인 BGM을 정지합니다.

---

## Global.SFX - SFXManager
- **SFXManager**는 효과음(SFX) 재생을 관리하는 싱글턴 매니저 클래스입니다.  
  오디오 소스를 통해 효과음을 재생할 수 있습니다.

### 주요 필드 및 프로퍼티
- **_audioSource**: SFX 재생에 사용되는 AudioSource 컴포넌트입니다.

### 주요 메서드
- **PlaySFX(AudioClip clip, float volume = 1.0f)**: 지정한 오디오 클립을 효과음으로 재생합니다.

---

## Global.Resource - ResourceManager

`ResourceManager`는 Unity의 `Resources` 폴더 내 리소스를 비동기로 로드/언로드/캐싱하는 싱글턴 매니저 클래스입니다.

### 주요 필드 및 프로퍼티

- **_cache**: 단일 오브젝트 캐시 딕셔너리 (`Dictionary<string, Object>`)
- **_cacheAll**: 다중 오브젝트 캐시 딕셔너리 (`Dictionary<string, Object[]>`)

### 주요 메서드

- **LoadAsync\<T\>(string path)**  
  지정한 경로의 리소스를 비동기로 로드하고 캐싱합니다.

- **LoadAllAsync\<T\>(string[] paths)**  
  여러 경로의 리소스를 비동기로 배열로 로드하고 캐싱합니다.

- **Unload(string path)**  
  지정한 경로의 리소스를 언로드하고 캐시에서 제거합니다.

- **UnloadAll()**  
  모든 캐시된 리소스를 언로드하고 캐시를 비웁니다.

- **NormalizeResourcePath(string path)**  
  Resources 경로를 항상 Resources 하위 상대경로로 변환합니다.

---

> 이 매니저는 Addressable Asset System을 사용하지 않는 프로젝트에서  
> Resources 기반 리소스 관리를 효율적으로 지원합니다.  
> Addressable Asset System 사용 시, 해당 매니저는 불필요합니다.

---

## Global.Coroutine - CoroutineManager
- **CoroutineManager**는 Unity에서 코루틴 기반 작업을 쉽게 관리할 수 있도록 도와주는 싱글턴 매니저 클래스입니다.  
  코루틴 시작, 반복, 중단(즉시/지연), 이벤트 기능을 제공합니다.

### 주요 메서드
- **Coroutine Run(float delay, Action callback, bool repeat = false, 
            Action onStart = null, Action onComplete = null,
            Action<float, float> onProgress = null)**
  지정한 시간 후 콜백을 실행하는 타이머를 시작합니다. 반복 실행도 지원합니다.  
  코루틴이 시작과 종료, 진행 중일 때 이벤트 호출을 지원합니다.
- **Stop(Coroutine timerCoroutine, float delayAfterStop, Action onStop = null)**  
  진행 중인 타이머 코루틴을 즉시 또는 일정 시간 후 중단합니다.
  코루틴이 종료될 때 이벤트 호출을 지원합니다.

### 예시

```csharp
// 2초 후 한 번 실행
Coroutine coroutine = Global.Coroutine.Run(2f, () => Debug.Log("2초 후 실행"));

// 1초마다 반복 실행
Coroutine repeatCoroutine = Global.Coroutine.Run(1f, () => Debug.Log("Tick"), true);

// 5초 후 반복 타이머 중단
Global.Coroutine.Stop(repeatCoroutine, 5f, () => Debug.Log("Stopped ticking after 5 seconds"));

// 5초 후에 실행되는 코루틴
// 진행 상황과 시작/완료 콜백 포함
Coroutine coroutine = Global.Coroutine.Run(5f,
  () => Debug.Log("5 seconds passed!"), 
  false,
  onStart: () => Debug.Log("Coroutine started"),
  onComplete: () => Debug.Log("Coroutine completed"),
  onProgress: (elapsedTime, progress) => 
  Debug.Log($"Elapsed Time: {elapsedTime}, Progress: {progress * 100}%"));
```
- Stop의 delayAfterStop을 0f로 주면 즉시 중단과 동일합니다.
- 반복 타이머는 반드시 외부에서 중단해주어야 합니다.
  
