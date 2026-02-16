# 🦾 PhikozzLibrary

간편하게 확장 가능한 Unity 게임 개발용 C# 라이브러리입니다.  
게임 개발 필수 유틸리티와 매니저, 서비스 구조가 포함되어 있습니다.

---

## 📚 목차

- [ServiceLocater](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/ServiceLocater)  
- [Singleton](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Singleton)  
- [Game](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Game)  
- [Audio](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Audio)  
- [Event](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Event)  
- [Pool](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Pool)  
- [Resource](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Resource)  
- [Save](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Save)  
- [UI](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/UI)  
- [Preinitialize](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/Preinitialize)  
- [FSM](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Runtime/FSM)  
- [Editor](https://github.com/Leekiahn/PhikozzLibrary/tree/main/Editor)  

---

## 서비스 등록 전 Null 발생 해결 방법

만약 아래와 같이 서비스를 사용하기 전에 등록이 되지 않아 `Null`이 발생할 경우에는 다음 방법을 사용하세요:

```csharp
await UniTask.WaitUntil(() => ServiceLocator.Get<ISaveService>() != null);
```

서비스가 등록될 때까지 기다린 뒤 메서드를 호출하면 `Null` 예외를 방지할 수 있습니다.

---

## 패키지 직접 코드 수정

패키지 내 코드를 수정해야 하는 경우, Unity 패키지 캐시 경로(`Library/PackageCache/com.phikozz.phikozzlibrary`)의 해당 폴더를 프로젝트의 `Packages` 폴더로 이동한 뒤 코드 수정을 진행하세요.

---

## 🗃️ 의존성

- [![Addressable](https://img.shields.io/badge/Addressable-Asset%20Management-brightgreen)](https://docs.unity3d.com/kr/current/Manual/com.unity.addressables.html)
- [![UniTask](https://img.shields.io/badge/UniTask-Async%20Utility-orange)](https://github.com/Cysharp/UniTask)
