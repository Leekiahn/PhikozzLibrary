# Save

게임 데이터의 저장 및 로드 기능을 위한 매니저, 인터페이스, 데이터 구조 클래스들을 포함합니다.

---

## 주요 파일

- [SaveManager.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a5f3227ccab8521bdef2265259122a6c7daab578/Runtime/Save/SaveManager.cs#L1-L66)
  - 싱글톤 글로벌 매니저로, 제네릭 타입 저장/로드 지원
  - ServiceLocator 연동 및 비동기 초기화 패턴
  - MyDocuments/회사명 경로에 JSON 형식 파일 저장

- [ISaveService.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a5f3227ccab8521bdef2265259122a6c7daab578/Runtime/Save/ISaveService.cs#L1-L8)
  - 저장/로드 메소드 정의 인터페이스

- [SaveData.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a5f3227ccab8521bdef2265259122a6c7daab578/Runtime/Save/SaveData.cs#L1-L19)
  - 예시 데이터 구조
  - PlayerData, SettingsData, 저장 시간 포함

---

## 사용 예시

### 데이터 저장

```csharp
Global.Save.Save("save1", new SaveData { PlayerData = ..., SettingsData = ... });
```

### 데이터 로드

```csharp
var save = Global.Save.Load<SaveData>("save1");
```

### 저장 경로

- MyDocuments/회사명/save1.json 형태로 저장됩니다.

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/2121f37045b6d8d2ae47d5725deeb6ff960de2ca/Runtime/Save)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
