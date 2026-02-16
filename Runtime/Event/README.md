# Event

게임 내 이벤트 시스템을 쉽고 효율적으로 구현할 수 있는 인터페이스와 매니저를 제공합니다.

---

## 주요 파일

- [IGameEvent.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/3aa407a725e721eeb49ed05c9cb9fbbfe6e73f3e/Runtime/Event/IGameEvent.cs#L1-L6)  
  - 게임 이벤트 타입의 마커 인터페이스로, 구독/발행 대상이 되는 이벤트 객체의 타입 기준입니다.

- [IEventService.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/3aa407a725e721eeb49ed05c9cb9fbbfe6e73f3e/Runtime/Event/IEventService.cs#L1-L11)  
  - 이벤트 구독, 구독 해제, 발행 메소드 정의.

- [EventManager.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/3aa407a725e721eeb49ed05c9cb9fbbfe6e73f3e/Runtime/Event/EventManager.cs#L1-L56)  
  - 싱글톤 글로벌 매니저 기반의 이벤트 처리 클래스
  - 딕셔너리로 타입별 델리게이트 관리, 다양한 이벤트 구독/발행/해제 로직 제공
  - ServiceLocator에 서비스 등록 및 비동기 초기화

---

## 사용 예시

### 1. 이벤트 클래스 정의

```csharp
public class PlayerDeadEvent : IGameEvent
{
    // 플레이어 사망 관련 정보 포함 가능
}
```

### 2. 구독(Subscribe)

```csharp
Global.Event.Subscribe<PlayerDeadEvent>(OnPlayerDead);

void OnPlayerDead(PlayerDeadEvent evt) {
    // 이벤트 처리 로직
}
```

### 3. 발행(Publish)

```csharp
Global.Event.Publish(new PlayerDeadEvent());
```

### 4. 구독 해제(Unsubscribe)

```csharp
Global.Event.Unsubscribe<PlayerDeadEvent>(OnPlayerDead);
```

---

## 비동기 초기화 및 서비스 연동

```csharp
await EventManager.Instance.InitAsync(); // ServiceLocator에 IEventService 등록
```

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/a9d74648c5032f3c89ff0ce0e4b58f3b96c38220/Runtime/Event)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
