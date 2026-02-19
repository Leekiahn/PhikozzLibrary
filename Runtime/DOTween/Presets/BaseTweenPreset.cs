using DG.Tweening;
using UnityEngine;

namespace PhikozzLibrary
{
    public abstract class BaseTweenPreset : ScriptableObject
    {
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        [SerializeField] private bool _loop;
        [SerializeField] private LoopType _loopType;

        public float GetDuration() => _duration;
        public Ease GetEase() => _ease;
        public bool GetLoop() => _loop;
        public LoopType GetLoopType() => _loopType;
    }
}
