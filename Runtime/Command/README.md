# Runtime/Command

이 디렉터리는 **커맨드 패턴(Command Pattern)** 구현을 위한 C# 코드들을 포함하고 있습니다. 커맨드 패턴은 실행할 명령을 객체로 캡슐화해 명령의 실행, 취소(Undo), 재실행(Redo) 등을 유연하게 관리할 수 있는 디자인 패턴입니다.

## 주요 구성 요소

- **ICommand 인터페이스**  
  커맨드 객체들이 반드시 구현해야 하는 기본 구조를 정의합니다.  
  일반적으로 `Execute()`, `Undo()` 등의 메서드를 포함합니다.

- **CommandInvoker**  
  커맨드의 실행, 실행 취소(Undo), 재실행(Redo) 등 전체적인 커맨드 흐름을 관리합니다.

- **Concrete Command**

- **Command Receiver**

## 사용 예시

```csharp
// ICommand 구현 예시
public class SampleConcreteCommand : ICommand
{
    private Transform _target;
    private Vector3 _direction;
    private float _distance;
    private Vector3 _previousPosition;

    public SampleConcreteCommand(Transform target, Vector3 direction, float distance)
    {
        _target = target;
        _direction = direction.normalized;
        _distance = distance;
    }

    public void Execute()
    {
        _previousPosition = _target.position;
        _target.position += _direction * _distance;
    }

    public void Undo()
    {
        _target.position = _previousPosition;
    }
}
```

```csharp
// CommandReceiver 구현 예시
public class CommandReceiver : MonoBehaviour
{
    public Transform player;
    CommandInvoker invoker;

    void Start()
    {
        invoker = new CommandInvoker();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            invoker.ExecuteCommand(new SampleConcreteCommand(player, Vector3.right, 1f));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            invoker.ExecuteCommand(new SampleConcreteCommand(player, Vector3.left, 1f));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            invoker.UndoCommand();
        }
    }
}
```

## 활용 예시

- 되돌리기(Undo)/재실행(Redo) 시스템 구현
- 사용자 입력 및 행동 이력 관리
- 스택 기반 커맨드 기록 및 반복 처리
