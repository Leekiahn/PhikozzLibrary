using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace PhikozzLibrary
{
    [RequireComponent(typeof(EventTrigger))]
    public class ScaleTweenAnimator : BaseTweenAnimator
    {
        private Transform _target => transform;
        private Tween _currentTween;

        public List<TweenData> scaleTweenDataList = new List<TweenData>();

        public override void Play(string key)
        {
            var data = scaleTweenDataList.Find(x => x.key == key);

            _currentTween?.Kill();

            var preset = data.tweenPreset as ScaleTweenPreset;

            var tween = _target.DOScale(preset.GetEndValue(), preset.GetDuration())
                .SetEase(preset.GetEase());

            if (preset.GetLoop())
            {
                tween.SetLoops(-1, preset.GetLoopType());
            }

            _currentTween = tween;
        }
    }
}