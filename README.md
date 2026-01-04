# :notebook: PhikozzLibrary
PhikozzLibrary는 Unity 개발 속도 향상을 위한 라이브러리입니다.
  
<br />
  
## Editor - 커스텀 에디터
### Project Setting Window - 프로젝트 폴더 셋업
- PhikozzLibrary/Project Setting Window 메뉴를 통해 버튼을 클릭하면 표준 폴더 구조를 자동으로 생성합니다.
- ResourceManager의 기본 경로는 Resources 폴더입니다.
```
Assets/  
├── 01.Scenes/  
├── 02.Scripts/  
├── 03.Prefabs/  
├── 04.Art/  
├── 05.Animations/  
├── 06.Audio/  
├── 07.Plugins/  
└── Resources/  
```
<br />

## Runtime - 기능 스크립트
### Global - 매니저 중앙화
- 각 매니저 싱글턴 인스턴스에 쉽게 접근할 있도록 Static 프로퍼티로 노출하며 중앙화되어 있습니다.
  
```Csharp
ex)
Global.Game.PauseGame();
Global.Resource.Load<AudioClip>("Audio/DM-CGS-27");
Global.UI.ShowPanel("InventoryPanel");
Vector2 moveInput = Global.Input.moveInput;
```
[Global README](https://github.com/Leekiahn/PhikozzLibrary/blob/main/Runtime/Manager/README.md)
<br />

### FSM - 유한상태머신
- 상태 전이(Enter, Tick, Exit)를 관리하는 상태 머신 추상 클래스입니다.  
  다양한 상태(`IState` 구현체)를 등록하고, 상태 전환 및 상태별 로직을 처리할 수 있습니다.

[FSM README](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/FSM)

### ObjectPool - 오브젝트 풀링
- 오브젝트 재사용을 위한 오브젝트 풀 클래스입니다.  
  오브젝트의 생성, 반환, 초기화 시 커스텀 동작을 지정할 수 있습니다.

[ObjectPool README](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Pooling)
