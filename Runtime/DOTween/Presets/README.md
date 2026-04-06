# Tween Presets

DOTween 애니메이션 설정값을 `ScriptableObject`로 분리해 재사용할 수 있도록 하는 프리셋 시스템입니다.

---

## 주요 파일

- [`BaseTweenPreset.cs`](./BaseTweenPreset.cs)  
  - 모든 트윈 프리셋의 공통 베이스 클래스입니다.
  - 재생 시간, Ease, 루프 여부, 루프 타입을 정의합니다.

- [`ScaleTweenPreset.cs`](./ScaleTweenPreset.cs)  
  - 스케일 애니메이션용 프리셋입니다.
  - 최종 스케일 값(`Vector3`)을 저장합니다.

- [`FadeTweenPreset.cs`](./FadeTweenPreset.cs)  
  - 페이드 애니메이션용 프리셋입니다.
  - 최종 알파값(`float`)을 저장합니다.

---

## BaseTweenPreset

모든 프리셋은 `BaseTweenPreset`을 상속합니다.

```csharp
public abstract class BaseTweenPreset : ScriptableObject
{
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    [SerializeField] private bool _loop;
    [SerializeField] private LoopType _loopType;

    public float GetDuration() => _duration;
    public Ease GetEase() => _ease;
    public bool GetLoop() => _loop;
    public LoopType GetLoopType() => _loopType;
}
```

### 공통 설정값

- `Duration`  
  - 트윈이 완료되기까지 걸리는 시간입니다.

- `Ease`  
  - DOTween의 easing 타입입니다.

- `Loop`  
  - 반복 재생 여부입니다.

- `LoopType`  
  - 반복 방식입니다. 예: Restart, Yoyo 등

---

## 제공 프리셋

### 1. ScaleTweenPreset

```csharp
[CreateAssetMenu(fileName = "ScaleTweenPreset", menuName = "PhikozzLibrary/TweenPresets/Scale")]
public class ScaleTweenPreset : BaseTweenPreset
{
    [Header("Scale")]
    [SerializeField] private Vector3 _endValue;

    public Vector3 GetEndValue() => _endValue;
}
```

#### 역할
- 스케일 애니메이션의 목표값을 정의합니다.
- `ScaleTweenAnimator`에서 사용됩니다.

#### 생성 경로
`Create > PhikozzLibrary > TweenPresets > Scale`

#### 예시
- 기본 크기: `(1, 1, 1)`
- Hover 확대: `(1.1, 1.1, 1.1)`
- 클릭 축소: `(0.9, 0.9, 0.9)`

---

### 2. FadeTweenPreset

```csharp
[CreateAssetMenu(fileName = "FadeTweenPreset", menuName = "PhikozzLibrary/TweenPresets/Fade")]
public class FadeTweenPreset : BaseTweenPreset
{
    [Header("Fade")]
    [SerializeField] private float _endAlpha = 1f;

    public float GetEndAlpha() => _endAlpha;
}
```

#### 역할
- 페이드 애니메이션의 목표 알파값을 정의합니다.
- `FadeTweenAnimator`에서 사용됩니다.

#### 생성 경로
`Create > PhikozzLibrary > TweenPresets > Fade`

#### 예시
- Fade In: `1.0`
- Fade Out: `0.0`

---

## 장점

### 1. 재사용 가능
동일한 애니메이션 설정을 여러 오브젝트에서 공통으로 사용할 수 있습니다.

예:
- 여러 버튼이 같은 Hover 스케일 프리셋 공유
- 여러 UI 패널이 같은 Fade In/Out 프리셋 공유

### 2. 데이터와 로직 분리
애니메이션 수치와 재생 로직을 분리해 유지보수가 쉬워집니다.

- 수치 설정: `TweenPreset`
- 재생 처리: `TweenAnimator`

### 3. Inspector 친화적
코드 수정 없이 아티스트/기획자가 프리셋 에셋만 조정해 애니메이션 감각을 변경할 수 있습니다.

---

## 사용 흐름

### 1. 프리셋 생성
프로젝트 창에서 프리셋 에셋을 생성합니다.

- `ScaleTweenPreset`
- `FadeTweenPreset`

### 2. 값 설정
Inspector에서 아래 값을 설정합니다.

- Duration
- Ease
- Loop
- LoopType
- 타입별 전용 값
  - Scale: `End Value`
  - Fade: `End Alpha`

### 3. Animator에 연결
Animator 컴포넌트의 `TweenData` 리스트에 키와 함께 프리셋을 연결합니다.

예시:
- `"Hover"` → `ScaleTweenPreset`
- `"Show"` → `FadeTweenPreset`

### 4. 키로 재생
런타임에 Animator의 `Play("키")`를 호출합니다.

---

## 사용 예시

### Scale 프리셋 예시

```csharp
[SerializeField] private ScaleTweenAnimator _scaleAnimator;

public void PlayHover()
{
    _scaleAnimator.Play("Hover");
}
```

### Fade 프리셋 예시

```csharp
[SerializeField] private FadeTweenAnimator _fadeAnimator;

public void Show()
{
    _fadeAnimator.Play("Show");
}

public void Hide()
{
    _fadeAnimator.Play("Hide");
}
```

---

## 연결 관계

- `ScaleTweenPreset` → `ScaleTweenAnimator`
- `FadeTweenPreset` → `FadeTweenAnimator`

즉 프리셋은 직접 실행되지 않고, 반드시 Animator를 통해 사용됩니다.

---

## 주의사항

### 1. Animator와 프리셋 타입을 맞춰야 합니다
현재 구현은 런타임 캐스팅을 사용합니다.

예:
- `ScaleTweenAnimator` 내부에서 `ScaleTweenPreset`으로 캐스팅
- `FadeTweenAnimator` 내부에서 `FadeTweenPreset`으로 캐스팅

따라서 타입이 맞지 않는 프리셋을 연결하면 null 참조 오류가 발생할 수 있습니다.

### 2. 시작값은 프리셋에 없습니다
현재 프리셋은 "목표값"만 저장합니다.

즉:
- Scale은 현재 Transform 스케일에서 `_endValue`로 이동
- Fade는 현재 알파에서 `_endAlpha`로 이동

초기 상태는 오브젝트/컴포넌트의 현재 값에 따라 결정됩니다.

---

## 참고

- Animator 구현: [`../Animators/README.md`](../Animators/README.md)
- DOTween 타입:
  - `Ease`
  - `LoopType`
