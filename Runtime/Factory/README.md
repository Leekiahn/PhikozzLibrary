# Runtime/Factory

이 디렉터리는 Unity 개발에서 객체의 생성을 일관적이고 효율적으로 처리하기 위해 **Factory Pattern(팩토리 패턴)**을 적용한 기능을 제공합니다.

## 목적

- 프리팹(혹은 컴포넌트)의 생성을 반복해서 사용할 때 코드 중복을 줄이고, 생성 방식의 일관성을 보장합니다.
- 생성 로직을 분리하여 테스트 및 유지보수, 확장을 용이하게 만듭니다.

## 포함 파일

- `IFactory.cs`  
  UnityEngine.Component를 상속하는 타입에 대해, 프리팹을 받아 지정된 위치와 회전값으로 생성하는 인터페이스입니다.
  ```csharp
  public interface IFactory<T> where T : Component
  {
      T Create(T prefab, Vector3 position, Quaternion rotation);
  }
  ```

- `ExampleFactory.cs`  
  `IFactory<T>`를 구현한 기본 예시 클래스입니다. 프리팹을 `Instantiate` 하여 실제로 게임 오브젝트를 생성합니다.
  ```csharp
  public class ExampleFactory<T> : IFactory<T> where T : Component
  {
      public T Create(T prefab, Vector3 position, Quaternion rotation)
      {
          return Instantiate(prefab, position, rotation);
      }
  }
  ```
