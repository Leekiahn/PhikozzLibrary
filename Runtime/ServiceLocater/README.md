# ServiceLocater

Unity 프로젝트의 서비스 및 매니저 인스턴스 등록/조회/해제를 위한 ServiceLocator 패턴과 초기화 관리 기능을 제공합니다.

---

## 주요 파일

- [ServiceLocator.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/d25e24537e7b4552d98b0b8eeecc6662269d85d6/Runtime/ServiceLocater/ServiceLocator.cs#L1-L46)  
  - 정적 딕셔너리로 서비스 객체를 등록하고, 타입별 조회 및 해제를 지원합니다.
  - `Register<T>(T service)`, `Get<T>()`, `Unregister<T>()` 메소드를 제공합니다.

- [ServiceBootstrapper.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/d25e24537e7b4552d98b0b8eeecc6662269d85d6/Runtime/ServiceLocater/ServiceBootstrapper.cs#L1-L40)
  - Unity 씬 내에서 관리할 매니저 프리팹을 Instantiate하고, 주요 매니저의 비동기 초기화를 수행합니다.
  - 각 매니저의 `InitAsync()` 결과를 로그로 확인할 수 있습니다.

---

## ServiceLocator 예시

```csharp
// 서비스 등록
ServiceLocator.Register<MyManager>(new MyManager());

// 서비스 사용
var manager = ServiceLocator.Get<MyManager>();

// 서비스 해제
ServiceLocator.Unregister<MyManager>();
```

---

## Bootstrapper 사용 시 초기화 로그 예시

```plaintext
✅ GameManager 초기화 성공
❌ ResourceManager 초기화 실패
...
```

---

## 참고 사항
- UnityEngine, Cysharp.Threading.Tasks 및 다양한 매니저 클래스(예: GameManager, ResourceManager 등)에 의존합니다.
- [전체 Source 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/a98d199e24256b9b6e0d6c0becfd0b5c1e60bbe5/Runtime/ServiceLocater)
