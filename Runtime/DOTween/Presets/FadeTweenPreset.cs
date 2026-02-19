using UnityEngine;

namespace PhikozzLibrary
{
    [CreateAssetMenu(fileName = "FadeTweenPreset", menuName = "PhikozzLibrary/TweenPresets/Fade")]
    public class FadeTweenPreset : BaseTweenPreset
    {
        [Header("Fade")]
        [SerializeField] private float _endAlpha = 1f;

        public float GetEndAlpha() => _endAlpha;
    }
}