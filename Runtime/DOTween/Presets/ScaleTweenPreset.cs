using UnityEngine;

[CreateAssetMenu(fileName = "ScaleTweenPreset", menuName = "PhikozzLibrary/TweenPresets/Scale")]
public class ScaleTweenPreset : BaseTweenPreset
{
    [Header("Scale")]
    [SerializeField] private Vector3 _endValue;
    
    public Vector3 GetEndValue() => _endValue;
}
