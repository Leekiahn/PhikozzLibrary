# Event

게임 내에서 타입 기반으로 이벤트를 구독, 해제, 발행할 수 있는 간단한 이벤트 시스템을 제공합니다.

---

## 주요 파일

- [`EventManager.cs`](./EventManager.cs)  
  - `EventManager : SingletonGlobal<EventManager>, IEventService, IInitializable`
  - 내부적으로 `Dictionary<Type, Delegate>`를 사용해 이벤트 핸들러를 타입별로 관리합니다.
  - `Subscribe<T>()`, `Unsubscribe<T>()`, `UnSubscribeAll<T>()`, `Publish<T>()`를 제공합니다.
  - `Init()`에서 `ServiceLocator`에 `IEventService`를 등록합니다.

- [`IEventService.cs`](./IEventService.cs)  
  - 이벤트 서비스 인터페이스입니다.
  - 구독, 구독 해제, 전체 해제, 발행 메소드를 정의합니다.

- [`../Global.cs`](../Global.cs)  
  - `Global.Event`를 통해 등록된 `IEventService`에 접근할 수 있습니다.

- [`../ServiceLocater/RuntimeServiceBootstrap.cs`](../ServiceLocater/RuntimeServiceBootstrap.cs)  
  - 런타임 시작 시 매니저 프리팹을 생성하고 `Init()`을 호출해 서비스를 등록합니다.

---

## 특징

- 이벤트 타입에 별도 인터페이스 제약이 없습니다.
- `class`, `struct`, `string` 등 어떤 타입이든 이벤트 타입으로 사용할 수 있습니다.
- 같은 타입 `T` 기준으로 핸들러가 묶여 관리됩니다.

---

## 사용 예시

### 1. 이벤트 타입 정의

```csharp
public class PlayerDieEvent
{
    public string PlayerName { get; }
    public int PlayerScore { get; }

    public PlayerDieEvent(string playerName, int playerScore)
    {
        PlayerName = playerName;
        PlayerScore = playerScore;
    }
}
```

### 2. 구독

```csharp
private void OnEnable()
{
    Global.Event.Subscribe<PlayerDieEvent>(OnPlayerDie);
}
```

### 3. 발행

```csharp
Global.Event.Publish(new PlayerDieEvent("Jack", 20));
```

### 4. 구독 해제

```csharp
private void OnDisable()
{
    Global.Event.Unsubscribe<PlayerDieEvent>(OnPlayerDie);
}
```

### 5. 특정 타입 전체 구독 해제

```csharp
Global.Event.UnSubscribeAll<PlayerDieEvent>();
```

---

## 초기화 및 서비스 연동

`EventManager`는 `Init()` 호출 시 `ServiceLocator.Register<IEventService>(this)`를 수행합니다.

프로젝트에서는 `RuntimeServiceBootstrap`이 실행되면서 `Resources/ManagerBootstrapConfig`에 등록된 매니저 프리팹을 생성하고, 각 `IInitializable`의 `Init()`을 호출합니다.  
초기화가 끝난 뒤에는 아래처럼 사용할 수 있습니다.

```csharp
Global.Event.Publish("Event service ready");
```

---

## 참고

- 이벤트를 사용하기 전에 `IEventService`가 등록되어 있어야 합니다.
- `Global.Event`가 `null`이라면 부트스트랩 설정 또는 매니저 프리팹 등록 상태를 먼저 확인하세요.
