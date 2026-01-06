# Runtime/Strategy

이 디렉터리는 **Strategy 패턴**을 활용하는 예시와 관련 클래스들을 포함하고 있습니다. 유니티(GameObject) 오브젝트에 전략(Strategy)을 동적으로 할당하고 실행할 수 있도록 구조화되어 있습니다.

## 파일 구성 및 설명

---

### 1. `IStrategy.cs`
- **역할:** 제너릭 전략 인터페이스 정의
- **설명:** Strategy 패턴에서 사용되는 전략(메서드)을 정의하는 인터페이스입니다. 
  - `Execute(T context)` 메서드를 통해 다양한 컨텍스트 객체(T)에 전략을 적용할 수 있습니다.

---

### 2. `StrategyContext.cs`
- **역할:** 전략 설정 및 실행을 담당하는 컨텍스트 클래스(제너릭)
- **설명:** 
  - 외부에서 `IStrategy<T>` 타입의 전략 객체를 받아 컨텍스트에서 전략을 변경(`SetStrategy`)하거나 실행(`ExecuteStrategy`)할 수 있습니다.
  - 실제 전략 객체는 내부 필드로 보관합니다.

---

### 3. `ExampleStrategy.cs`
- **역할:** 예시 전략 클래스
- **설명:** `IStrategy<GameObject>`를 구현한 예시로, `GameObject`를 대상으로 실행 시 로그를 남깁니다.
  - 새로운 전략을 구현할 때 참고할 수 있는 기본 예제입니다.

---

### 4. `ExampleContext.cs`
- **역할:** 예제 Context(컨텍스트) MonoBehaviour
- **설명:** 
  - 유니티 환경에서 `StrategyContext<GameObject>`를 활용하여 동적으로 전략을 설정하고, 현재 게임 오브젝트에 전략을 실행할 수 있습니다.
  - 실제 프로젝트에서 `SetStrategy`, `ExecuteStrategy` 메서드를 호출해 전략을 변경 및 실행하는 구조의 예시입니다.

---

## 예시 사용법

```csharp
// Context에서 전략 등록 및 실행 예시
var strategyContext = new StrategyContext<GameObject>();
strategyContext.SetStrategy(new ExampleStrategy());
strategyContext.ExecuteStrategy(gameObject); // 로그 출력
```

---

## 참고

- 이 구조는 다양한 행동(전략)을 동적으로 교환, 확장하고자 할 때 유용합니다.
- 신규 전략을 만들고 싶다면 `IStrategy<T>`를 구현하세요.
