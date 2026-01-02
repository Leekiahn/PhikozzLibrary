# PhikozzLibrary

PhikozzLibrary는 Unity 개발 속도 향상을 위한 라이브러리입니다.

## Global 클래스
- 각 매니저 싱글턴 인스턴스에 쉽게 접근할 있도록 중앙화
```csharp
Global.Game.PauseGame();
Global.Resource.Load<AudioClip>("Audio/DM-CGS-27");
Global.UI.ShowPanel("InventoryPanel");
Vector2 moveInput = Global.Input.moveInput;
```
