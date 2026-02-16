# UI

게임 내 UI 패널 관리 및 인터페이스 기능을 제공합니다.

---

## 주요 파일

- [UIManager.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a9d74648c5032f3c89ff0ce0e4b58f3b96c38220/Runtime/UI/UIManager.cs#L1-L61)
  - 싱글톤 글로벌 매니저로, 여러 패널의 등록/해제/표시/숨김 기능을 종류별로 제공합니다.
  - ServiceLocator 연동 및 비동기 초기화 패턴 적용

- [BaseUIPanel.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a9d74648c5032f3c89ff0ce0e4b58f3b96c38220/Runtime/UI/BaseUIPanel.cs#L1-L12)
  - UI 패널의 추상 클래스, Open/Close 메소드 제공
  - 각 패널은 해당 기능을 오버라이드 가능

- [IUIService.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a9d74648c5032f3c89ff0ce0e4b58f3b96c38220/Runtime/UI/IUIService.cs#L1-L10)
  - UI 패널 관리 인터페이스

---

## 사용 예시

### 패널 등록 및 해제

```csharp
Global.UI.RegisterPanel(myPanel);
Global.UI.UnregisterPanel<MyPanel>();
```

### 패널 표시/숨김

```csharp
Global.UI.ShowPanel<MyPanel>();
Global.UI.HidePanel<MyPanel>();
```

### 패널 구현 예시

```csharp
public class MyPanel : BaseUIPanel
{
    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
```

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/dea1f771297857dd435b9835ca16804278d5c4f6/Runtime/UI)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
