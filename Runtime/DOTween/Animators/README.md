# Tween Animators

DOTween 프리셋을 기반으로 대상 오브젝트에 트윈 애니메이션을 재생하는 컴포넌트들을 제공합니다.

---

## 주요 파일

- [`BaseTweenAnimator.cs`](./BaseTweenAnimator.cs)  
  - 모든 Tween Animator의 베이스 클래스입니다.
  - `Play(string key)` 추상 메소드를 통해 키 기반 애니메이션 재생 인터페이스를 정의합니다.
  - `TweenData` 타입을 함께 제공하며, 각 항목은 `key`와 `BaseTweenPreset` 참조를 가집니다.

- [`ScaleTweenAnimator.cs`](./ScaleTweenAnimator.cs)  
  - `Transform`의 스케일을 DOTween으로 애니메이션합니다.
  - 내부적으로 `transform.DOScale(...)`를 사용합니다.
  - 실행 전 이전 트윈을 종료하고, 지정된 프리셋의 `duration`, `ease`, `loop`, `loopType`을 적용합니다.

- [`FadeTweenAnimator.cs`](./FadeTweenAnimator.cs)  
  - 알파값 기반 페이드 애니메이션을 처리합니다.
  - `CanvasGroup`, `Image`, `SpriteRenderer` 중 하나를 대상으로 삼을 수 있습니다.
  - 내부적으로 `DOFade(...)`를 사용하며, 대상 타입은 `eFadeTargetType`으로 선택합니다.

---

## 구조

### TweenData

```csharp
[Serializable]
public class TweenData
{
    public string key;
    public BaseTweenPreset tweenPreset;
}
```

Animator는 여러 개의 `TweenData`를 리스트로 보관하고, `Play(string key)` 호출 시 해당 키에 연결된 프리셋을 찾아 실행합니다.

---

## 제공 Animator

### 1. ScaleTweenAnimator

`ScaleTweenAnimator`는 현재 오브젝트의 `Transform`에 대해 스케일 트윈을 실행합니다.

```csharp
public override void Play(string key)
{
    var data = scaleTweenDataList.Find(x => x.key == key);

    _currentTween?.Kill();

    var preset = data.tweenPreset as ScaleTweenPreset;

    var tween = transform.DOScale(preset.GetEndValue(), preset.GetDuration())
        .SetEase(preset.GetEase());

    if (preset.GetLoop())
    {
        tween.SetLoops(-1, preset.GetLoopType());
    }

    _currentTween = tween;
}
```

#### 특징
- 대상: 현재 `GameObject`의 `Transform`
- 동작: 스케일 변경
- 루프 지원: O
- 이전 트윈 자동 종료: O

---

### 2. FadeTweenAnimator

`FadeTweenAnimator`는 선택된 대상 컴포넌트의 알파값을 변경합니다.

지원 대상:
- `CanvasGroup`
- `Image`
- `SpriteRenderer`

대상 타입은 `eFadeTargetType`으로 설정합니다.

```csharp
public enum eFadeTargetType
{
    CanvasGroup,
    SpriteRenderer,
    Image,
}
```

#### 특징
- 대상: `CanvasGroup`, `Image`, `SpriteRenderer`
- 동작: 알파값 변경
- 루프 지원: O
- 이전 트윈 자동 종료: O

---

## 사용 방법

### 1. 프리셋 생성

먼저 `TweenPreset` 에셋을 생성합니다.

- Scale용: `Create > PhikozzLibrary > TweenPresets > Scale`
- Fade용: `Create > PhikozzLibrary > TweenPresets > Fade`

프리셋 상세 내용은 [`../Presets/README.md`](../Presets/README.md)를 참고하세요.

---

### 2. 컴포넌트 추가

애니메이션할 오브젝트에 원하는 Animator를 추가합니다.

- 스케일 애니메이션: `ScaleTweenAnimator`
- 페이드 애니메이션: `FadeTweenAnimator`

두 Animator 모두 `[RequireComponent(typeof(EventTrigger))]`가 선언되어 있어 `EventTrigger` 컴포넌트를 함께 요구합니다.

---

### 3. 키와 프리셋 연결

Inspector의 리스트에 다음처럼 설정합니다.

예시:
- `Hover` → `ScaleTweenPreset`
- `Click` → `ScaleTweenPreset`
- `Show` → `FadeTweenPreset`
- `Hide` → `FadeTweenPreset`

---

### 4. 코드에서 재생

```csharp
public class ExampleTweenPlayer : MonoBehaviour
{
    [SerializeField] private ScaleTweenAnimator _scaleAnimator;
    [SerializeField] private FadeTweenAnimator _fadeAnimator;

    public void PlayHover()
    {
        _scaleAnimator.Play("Hover");
    }

    public void PlayShow()
    {
        _fadeAnimator.Play("Show");
    }
}
```

---

## 예시 시나리오

### 버튼 Hover 시 확대

- `ScaleTweenAnimator` 추가
- `Hover` 키에 확대용 `ScaleTweenPreset` 연결
- 포인터 엔터 시 `Play("Hover")` 호출

```csharp
public void OnPointerEnter()
{
    _scaleAnimator.Play("Hover");
}
```

### UI 표시 시 Fade In

- `FadeTweenAnimator` 추가
- `_targetType`을 `CanvasGroup` 또는 `Image`로 설정
- `Show` 키에 Fade In용 프리셋 연결
- UI 열릴 때 `Play("Show")` 호출

```csharp
public void Open()
{
    _fadeAnimator.Play("Show");
}
```

---

## 주의사항

### 1. 키가 없으면 예외가 발생할 수 있습니다
현재 구현은 `Find(...)` 결과가 없을 때 null 체크를 하지 않습니다.

즉, 다음 코드에서:

```csharp
var data = fadeTweenDataList.Find(x => x.key == key);
var preset = data.tweenPreset as FadeTweenPreset;
```

`data`가 없으면 런타임 오류가 발생할 수 있습니다.

따라서:
- Inspector에 올바른 키를 반드시 등록하고
- 코드에서 존재하는 키만 호출해야 합니다.

---

### 2. 프리셋 타입이 맞아야 합니다
- `ScaleTweenAnimator`에는 `ScaleTweenPreset`
- `FadeTweenAnimator`에는 `FadeTweenPreset`

을 연결해야 안전합니다.  
다른 프리셋을 연결하면 캐스팅 결과가 null이 되어 오류가 발생할 수 있습니다.

---

### 3. EventTrigger가 자동으로 Play를 호출해주지는 않습니다
`[RequireComponent(typeof(EventTrigger))]`는 컴포넌트 존재를 보장할 뿐, 이벤트 바인딩 로직까지 자동으로 구성하지는 않습니다.

즉 실제 재생은 아래 둘 중 하나로 직접 연결해야 합니다.

- 코드에서 `Play("key")` 호출
- Unity `EventTrigger` 또는 UnityEvent에서 `Play("key")` 연결

---

### 4. FadeTweenAnimator는 대상 컴포넌트가 필요합니다
`_targetType`에 따라 아래 컴포넌트가 실제로 붙어 있어야 애니메이션이 동작합니다.

- `CanvasGroup` 선택 시 `CanvasGroup`
- `Image` 선택 시 `Image`
- `SpriteRenderer` 선택 시 `SpriteRenderer`

---

## 참고

- DOTween 네임스페이스: `DG.Tweening`
- 페이드 대상 타입 enum: [`../../Enums/Enum.cs`](../../Enums/Enum.cs)
- 프리셋 정의: [`../Presets/README.md`](../Presets/README.md)
