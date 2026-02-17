using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class FadeTweenAnimator : BaseTweenAnimator
{
    [SerializeField] private eFadeTargetType _targetType;
    private CanvasGroup _canvasGroup;
    private Image _image;
    private SpriteRenderer _spriteRenderer;
    
    public List<TweenData> fadeTweenDataList = new List<TweenData>();
    private Tween _currentTween;
    
    public override void Play(string key)
    {
        var data = fadeTweenDataList.Find(x => x.key == key);
        if (data == null || data.tweenPreset == null)
            return;

        _currentTween?.Kill();

        var preset = data.tweenPreset as FadeTweenPreset;
        if (preset == null)
            return;

        switch (_targetType)
        {
            case eFadeTargetType.CanvasGroup:
                if (_canvasGroup == null)
                    _canvasGroup = GetComponent<CanvasGroup>();
                if (_canvasGroup != null)
                    _currentTween = _canvasGroup.DOFade(preset.GetEndAlpha(), preset.GetDuration())
                        .SetEase(preset.GetEase());
                break;
            case eFadeTargetType.Image:
                if (_image == null)
                    _image = GetComponent<Image>();
                if (_image != null)
                    _currentTween = _image.DOFade(preset.GetEndAlpha(), preset.GetDuration())
                        .SetEase(preset.GetEase());
                break;
            case eFadeTargetType.SpriteRenderer:
                if (_spriteRenderer == null)
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                if (_spriteRenderer != null)
                    _currentTween = _spriteRenderer.DOFade(preset.GetEndAlpha(), preset.GetDuration())
                        .SetEase(preset.GetEase());
                break;
        }

        if (_currentTween != null && preset.GetLoop())
            _currentTween.SetLoops(-1, preset.GetLoopType());
    }
}