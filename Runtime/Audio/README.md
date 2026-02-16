# Audio

Unity에서 배경음악(BGM) 및 효과음(SFX) 관리에 사용되는 오디오 시스템 구현 파일들을 포함합니다.

---

## 주요 파일

- [AudioManager.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a62e79fb33a68d816b6c60b41b79445cb567a804/Runtime/Audio/AudioManager.cs#L1-L44)  
  - 싱글톤 기반의 글로벌 매니저(`SingletonGlobal<AudioManager>`)  
  - `IAudioService` 인터페이스와 `IPreinitialize` 초기화 인터페이스 구현
  - AudioSource를 통한 BGM/SFX 재생 및 서비스 등록, 비동기 초기화 제공

- [IAudioService.cs](https://github.com/Leekiahn/PhikozzLibrary/blob/a62e79fb33a68d816b6c60b41b79445cb567a804/Runtime/Audio/IAudioService.cs#L1-L11)  
  - 오디오 서비스의 핵심 인터페이스
  - BGM 및 SFX 재생, BGM 정지 메소드 제공

---

## AudioManager 기능 예시

```csharp
// AudioManager를 통한 BGM 재생
AudioManager.Instance.PlayBGM(myBGMClip);           // 배경음악 재생, 기본적으로 반복(loop)

// SFX(효과음) 재생
AudioManager.Instance.PlaySFX(mySFXClip);

// BGM 정지
AudioManager.Instance.StopBGM();
```

비동기 초기화 및 서비스 등록은 다음과 같이 이루어집니다:

```csharp
await AudioManager.Instance.InitAsync();  // AudioSource 존재 확인 및 ServiceLocator에 등록
```

---

## 관련 의존성

- UnityEngine
- Cysharp.Threading.Tasks
- PhikozzLibrary 내 ServiceLocator 및 싱글톤 베이스클래스

---

## 참고

- [전체 소스 보기](https://github.com/Leekiahn/PhikozzLibrary/tree/a5f3227ccab8521bdef2265259122a6c7daab578/Runtime/Audio)
- 코드 검색 제한으로 일부 파일만 포함될 수 있습니다. 더 많은 내용을 보려면 GitHub에서 직접 확인하세요.
