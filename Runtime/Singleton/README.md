# Singleton

Unity에서 쉽게 싱글톤 패턴을 구현할 수 있도록 돕는 Generic Base 클래스들을 제공합니다.

---

## 주요 파일

- [SingletonScene.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a62e79fb33a68d816b6c60b41b79445cb567a804/Runtime/Singleton/SingletonScene.cs#L1-L42)  
  - 씬 단위 싱글톤.  
  - 씬 내에 하나의 인스턴스만 존재하며, 이미 존재하면 추가 생성 시 기존 인스턴스가 유지되고 새로운 인스턴스는 파괴됩니다.

- [SingletonGlobal.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a62e79fb33a68d816b6c60b41b79445cb567a804/Runtime/Singleton/SingletonGlobal.cs#L1-L41)  
  - 글로벌(DontDestroyOnLoad) 싱글톤.  
  - 씬이 변경되어도 인스턴스가 유지되며, 중복 생성 시 기존 인스턴스 유지, 새 인스턴스 파괴.

---

## 사용 예시

```csharp
// SingletonScene 사용 예시
public class MySceneManager : SingletonScene<MySceneManager>
{
    // ...
}

// SingletonGlobal 사용 예시
public class MyGlobalManager : SingletonGlobal<MyGlobalManager>
{
    // ...
}
```

---

## 동작 원리

- Instance 프로퍼티를 통해 최초 접근 시 현재 씬에 존재하는 객체가 있는지 검사하여, 없으면 새로 생성.
- Awake()에서 중복 인스턴스가 생성될 경우 바로 파괴하여 단일성(singleton) 보장.
- SingletonGlobal은 DontDestroyOnLoad로 씬 이동 후에도 매니저 인스턴스를 유지시킴.

---

## 참고 사항

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/bde39a3af3c823d4cbd8a83e22a9167019ca6e3d/Runtime/Singleton)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
