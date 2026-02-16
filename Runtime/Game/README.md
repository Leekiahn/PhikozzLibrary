# Game

게임의 전반적 상태 및 진행 관리와 관련된 핵심 매니저와 인터페이스를 제공합니다.

---

## 주요 파일

- [GameManager.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/107d060049263de141aaef5cfd62f6f88de5b870/Runtime/Game/GameManager.cs)
  - 싱글톤 글로벌 매니저로, 게임 상태를 관리하고 초기화, 흐름 제어, 진행 정보 관리 기능을 제공합니다.
  - 비동기 초기화(InitAsync) 메소드와 ServiceLocator 연동 가능.

- [IGameService.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/107d060049263de141aaef5cfd62f6f88de5b870/Runtime/Game/IGameService.cs)
  - 게임 서비스의 인터페이스로, 주요 상태 관리 및 게임 흐름에 필요한 메소드 정의.

---

## 예시

### GameManager에서 초기화 및 진행 관리

```csharp
await GameManager.Instance.InitAsync();

// 게임 상태, 진행, 흐름 컨트롤 등
Global.Game.SetGameState(GameState.Playing);
```

### ServiceLocator를 통한 서비스 등록/조회

```csharp
var gameService = ServiceLocator.Get<IGameService>();
```

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/107d060049263de141aaef5cfd62f6f88de5b870/Runtime/Game)
- 검색 제한으로 일부 파일만 예시로 표시될 수 있습니다. 더 많은 내용은 GitHub에서 직접 확인하세요.
