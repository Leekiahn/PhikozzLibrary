using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "FadeTweenSetting", menuName = "PhikozzLibrary/DOTween/FadeTweenSetting")]
public class FadeTweenSetting : ScriptableObject
{
    [field: SerializeField] public float StartAlpha { get; private set; }
    [field: SerializeField] public float EndAlpha { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public Ease ease { get; private set; }
}
