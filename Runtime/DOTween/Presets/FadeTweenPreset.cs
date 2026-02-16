using UnityEngine;

namespace PhikozzLibrary
{
    [CreateAssetMenu(fileName = "FadeTweenPreset", menuName = "PhikozzLibrary/TweenPresets/Fade")]
    public class FadeTweenPreset : BaseTweenPreset
    {
        [Header("Fade")] 
        [SerializeField] private float _fade;

        [SerializeField] private bool _useInitFade;
        [SerializeField] private float _initFade;
        
        public float GetFade()
        {
            return _fade;
        }

        public bool GetUseInitFade()
        {
            return _useInitFade;
        }

        public float GetInitFade()
        {
            return _initFade;
        }
    }
}