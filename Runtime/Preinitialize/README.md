# Preinitialize

비동기 방식의 매니저 및 서비스 초기화 인터페이스를 제공합니다.

---

## 주요 파일

- [IPreinitialize.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a98d199e24256b9b6e0d6c0becfd0b5c1e60bbe5/Runtime/Preinitialize/IPreinitialize.cs#L1-L7)
  - 매니저/서비스 초기화를 위한 비동기 인터페이스
  - `UniTask<bool> InitAsync();` 메소드 정의

---

## 사용 예시

### 초기화 패턴

```csharp
public class AudioManager : IPreinitialize
{
    public async UniTask<bool> InitAsync()
    {
        // 비동기 초기화 코드
        return true;
    }
}
```

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/8f77065b5e65dcebdb6d2e61e99388846788ca51/Runtime/Preinitialize)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
