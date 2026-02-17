using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ScaleTweenAnimator : BaseTweenAnimator
{
    private Transform _target => transform;
    
    public List<TweenData> scaleTweenDataList = new List<TweenData>();
    private Tween _currentTween;

    public override void Play(string key)
    {
        var data = scaleTweenDataList.Find(x => x.key == key);
        if (data == null || data.tweenPreset == null || _target == null)
            return;

        // 기존 트윈 종료
        _currentTween?.Kill();

        var preset = data.tweenPreset as ScaleTweenPreset;
        if (preset == null)
            return;

        var tween = _target.DOScale(preset.GetEndValue(), preset.GetDuration())
            .SetEase(preset.GetEase());

        if (preset.GetLoop())
            tween.SetLoops(-1, preset.GetLoopType());

        _currentTween = tween;
    }
}
