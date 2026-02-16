using UnityEngine;
using DG.Tweening;

namespace PhikozzLibrary
{
    public class ScaleTweenAnimator : BaseTweenAnimator
    {
        [SerializeField] private ScaleTweenPreset _scaleTweenPreset;

        public override void DoFromInit()
        {
            DoScale(_scaleTweenPreset.GetUseInitScale() ? _scaleTweenPreset.GetInitScale() : (Vector3?)null, _scaleTweenPreset.GetScale());
        }

        public override void DoToInit()
        {
            DoScale(null, _scaleTweenPreset.GetInitScale());
        }

        private void DoScale(Vector3? fromScale, Vector3 toScale)
        {
            if (fromScale.HasValue)
                transform.localScale = fromScale.Value;

            transform.DOScale(toScale, _scaleTweenPreset.GetDuration())
                .SetEase(_scaleTweenPreset.GetEase())
                .SetLoops(_scaleTweenPreset.GetLoopCount(), _scaleTweenPreset.GetLoopType());
        }
    }
}