# Pool

Unity에서 객체 풀링(pooling) 시스템을 구현할 수 있는 인터페이스와 매니저를 포함합니다.

---

## 주요 파일

- [PoolManager.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a98d199e24256b9b6e0d6c0becfd0b5c1e60bbe5/Runtime/Pool/PoolManager.cs#L1-L116)
  - 싱글톤 글로벌 매니저로, 다양한 프리팹에 대한 ObjectPool 생성/관리/해제 기능을 제공합니다.
  - 서비스 초기화 및 ServiceLocator 연동 패턴 적용.

- [IPoolObject.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a98d199e24256b9b6e0d6c0becfd0b5c1e60bbe5/Runtime/Pool/IPoolObject.cs#L1-L10)
  - 풀링 대상 오브젝트가 구현해야 하는 라이프사이클 인터페이스(OnCreate, OnGet, OnRelease, OnDestroy).

- [IPoolService.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a98d199e24256b9b6e0d6c0becfd0b5c1e60bbe5/Runtime/Pool/IPoolService.cs#L1-L15)
  - 풀 관리 인터페이스로 다양한 오브젝트 풀 관련 메소드 정의.

---

## 사용 예시

### 객체 풀 생성

```csharp
Global.Pool.CreatePool(myPrefab, defaultCapacity: 10, maxSize: 50);
```

### 풀에서 오브젝트 가져오기

```csharp
var pool = Global.Pool.Get(myPrefab);
var obj = pool.Get();
```

### 오브젝트 반환(Release)

```csharp
Global.Pool.Release(myPrefab);     // 하나 반환
Global.Pool.ReleaseAll(myPrefab);  // 모두 반환
```

### 풀 삭제

```csharp
Global.Pool.DestroyAll(myPrefab);
```

### IPoolObject 구현 예시

```csharp
public class MyPoolObject : MonoBehaviour, IPoolObject
{
    public void OnCreate() { /* 생성 시 초기화 */ }
    public void OnGet()    { /* 풀에서 가져올 때 */ }
    public void OnRelease(){ /* 반환 시 행동 */ }
    public void OnDestroy(){ /* 파괴 전 처리 */ }
}
```

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/627148c44b6d4f506af5e3ba3af7a74e29e9e04f/Runtime/Pool)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
