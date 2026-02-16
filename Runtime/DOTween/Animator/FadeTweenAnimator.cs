using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace PhikozzLibrary
{
    public class FadeTweenAnimator : BaseTweenAnimator
    {
        [SerializeField] private FadeTweenPreset _fadeTweenPreset;

        private enum FadeTargetType { CanvasGroup, Image, SpriteRenderer }
        [SerializeField] private FadeTargetType _targetType = FadeTargetType.CanvasGroup;

        public override void DoFromInit()
        {
            DoFade(_fadeTweenPreset.GetUseInitFade() ? _fadeTweenPreset.GetInitFade() : (float?)null, _fadeTweenPreset.GetFade());
        }

        public override void DoToInit()
        {
            DoFade(null, _fadeTweenPreset.GetInitFade());
        }

        private void DoFade(float? fromAlpha, float toAlpha)
        {
            switch (_targetType)
            {
                case FadeTargetType.CanvasGroup:
                    var canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
                    if (fromAlpha.HasValue) canvasGroup.alpha = fromAlpha.Value;
                    canvasGroup.DOFade(toAlpha, _fadeTweenPreset.GetDuration())
                        .SetEase(_fadeTweenPreset.GetEase())
                        .SetLoops(_fadeTweenPreset.GetLoopCount(), _fadeTweenPreset.GetLoopType());
                    break;

                case FadeTargetType.Image:
                    var image = GetComponent<Image>();
                    if (image != null)
                    {
                        var color = image.color;
                        if (fromAlpha.HasValue) color.a = fromAlpha.Value;
                        image.color = color;
                        image.DOFade(toAlpha, _fadeTweenPreset.GetDuration())
                            .SetEase(_fadeTweenPreset.GetEase())
                            .SetLoops(_fadeTweenPreset.GetLoopCount(), _fadeTweenPreset.GetLoopType());
                    }
                    break;

                case FadeTargetType.SpriteRenderer:
                    var spriteRenderer = GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        var color = spriteRenderer.color;
                        if (fromAlpha.HasValue) color.a = fromAlpha.Value;
                        spriteRenderer.color = color;
                        spriteRenderer.DOFade(toAlpha, _fadeTweenPreset.GetDuration())
                            .SetEase(_fadeTweenPreset.GetEase())
                            .SetLoops(_fadeTweenPreset.GetLoopCount(), _fadeTweenPreset.GetLoopType());
                    }
                    break;
            }
        }
    }
}
