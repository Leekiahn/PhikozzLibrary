# Audio

`Audio` 모듈은 Unity에서 배경음악(BGM)과 효과음(SFX)을 재생하고 제어하기 위한 오디오 서비스입니다.

---

## 주요 파일

- [`AudioManager.cs`](./AudioManager.cs)
  - 글로벌 싱글톤 매니저 `SingletonGlobal<AudioManager>` 기반 구현
  - `IAudioService`, `IInitializable` 구현
  - `ServiceLocator`에 오디오 서비스를 등록
  - BGM 재생/정지/일시정지/재개, 볼륨 조절, 음소거 기능 제공

- [`IAudioService.cs`](./IAudioService.cs)
  - 오디오 시스템의 서비스 인터페이스
  - BGM/SFX 재생 및 제어 API 정의

---

## 초기화 방식

`AudioManager`는 직접 비동기로 초기화하는 구조가 아니라, 런타임 부트스트랩 과정에서 `IInitializable.Init()`이 호출되는 방식입니다.

관련 코드:
- [`RuntimeServiceBootstrap.cs`](../ServiceLocater/RuntimeServiceBootstrap.cs)
- [`IInitializable.cs`](../Preinitialize/IInitializable.cs)

즉, 일반 사용 코드에서 `AudioManager.Init()`을 직접 호출하기보다는  
부트스트랩 설정을 통해 자동 초기화되도록 사용하는 것을 전제로 합니다.

---

## 제공 기능

`IAudioService`를 통해 다음 기능을 사용할 수 있습니다.

- `bool IsPlayingBGM`
- `void PlayBGM(AudioClip clip, bool loop = true)`
- `void PlaySFX(AudioClip clip)`
- `void StopBGM()`
- `void PauseBGM()`
- `void ResumeBGM()`
- `void SetBGMVolume(float volume)`
- `void SetSFXVolume(float volume)`
- `void MuteBGM(bool mute)`
- `void MuteSFX(bool mute)`

볼륨 값은 내부적으로 `0~100` 범위를 받아 `0~1`로 변환한 뒤 `Mathf.Clamp01`으로 처리합니다.

---

## 사용 예시

```csharp
// BGM 재생
Global.Audio.PlayBGM(myBGMClip);

// 반복 없이 BGM 1회 재생
Global.Audio.PlayBGM(myBGMClip, false);

// SFX 재생
Global.Audio.PlaySFX(mySFXClip);

// BGM 정지
Global.Audio.StopBGM();

// BGM 일시정지 / 재개
Global.Audio.PauseBGM();
Global.Audio.ResumeBGM();

// 볼륨 조절 (0~100 기준)
Global.Audio.SetBGMVolume(50f);
Global.Audio.SetSFXVolume(80f);

// 음소거
Global.Audio.MuteBGM(true);
Global.Audio.MuteSFX(true);

// 현재 BGM 재생 여부 확인
bool isPlaying = Global.Audio.IsPlayingBGM;
```

---

## 설정 시 주의사항

`AudioManager`에는 두 개의 `AudioSource` 참조가 필요합니다.

- `_bgmSource`
- `_sfxSource`

따라서 `AudioManager`를 사용하는 프리팹 또는 오브젝트에서  
인스펙터를 통해 각 용도에 맞는 `AudioSource`를 연결해 두는 것이 좋습니다.

`[RequireComponent(typeof(AudioSource))]`는 최소 하나의 `AudioSource` 존재만 보장하므로,  
실제 사용 시에는 BGM/SFX용 소스를 명시적으로 구성하는 편이 안전합니다.

---

## 관련 파일

- [`Global.cs`](../Global.cs)
- [`AudioManager.cs`](./AudioManager.cs)
- [`IAudioService.cs`](./IAudioService.cs)
- [`RuntimeServiceBootstrap.cs`](../ServiceLocater/RuntimeServiceBootstrap.cs)
- [`IInitializable.cs`](../Preinitialize/IInitializable.cs)
