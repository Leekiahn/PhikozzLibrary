using UnityEngine;

namespace PhikozzLibrary
{
    [CreateAssetMenu(fileName = "ScaleTweenPreset", menuName = "PhikozzLibrary/TweenPresets/Scale")]
    public class ScaleTweenPreset : BaseTweenPreset
    {
        [Header("Scale")] 
        [SerializeField] private Vector3 _scale;
        
        [SerializeField] private bool _useInitScale;
        [SerializeField] private Vector3 _initScale;

        public Vector3 GetScale()
        {
            return _scale;
        }

        public bool GetUseInitScale()
        {
            return _useInitScale;
        }

        public Vector3 GetInitScale()
        {
            return _initScale;
        }
    }
}