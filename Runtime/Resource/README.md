# Resource

Unity Addressables 시스템 기반의 리소스 관리 기능을 제공합니다.

---

## 주요 파일

- [ResourceManager.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a5f3227ccab8521bdef2265259122a6c7daab578/Runtime/Resource/ResourceManager.cs#L1-L87)
  - Addressables를 사용하여 리소스 비동기 로드 및 해제 기능을 관리하는 싱글톤 글로벌 매니저
  - 단일 데이터(LoadAsync), 레이블 단위 데이터(LoadAllAsync) 비동기 로드, 키/레이블 해제(ReleaseByKey, ReleaseByLabel)
  - ServiceLocator 연동 및 비동기 초기화

- [IResourceService.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a5f3227ccab8521bdef2265259122a6c7daab578/Runtime/Resource/IResourceService.cs#L1-L14)
  - 주요 리소스 관리 인터페이스
  - 비동기 로드 및 메모리 해제, 레이블 접근 인터페이스 정의

---

## 사용 예시

### 리소스 비동기 로드

```csharp
var prefab = await Global.Resource.LoadAsync<GameObject>("PlayerPrefab");
var spriteList = await Global.Resource.LoadAllAsync<Sprite>("EnemySprites");
```

### 리소스 해제(메모리 반환)

```csharp
Global.Resource.ReleaseByKey("PlayerPrefab");
Global.Resource.ReleaseByLabel("EnemySprites");
```

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/2e5574c577842cc7879fa422b1cac0fff8382555/Runtime/Resource)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
