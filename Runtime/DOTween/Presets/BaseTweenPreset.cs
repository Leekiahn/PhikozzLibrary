using UnityEngine;
using DG.Tweening;

namespace PhikozzLibrary
{
    public abstract class BaseTweenPreset : ScriptableObject
    {
        [Header("Base")] 
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
    
        [Header("Loop")]
        [SerializeField] private bool _useLoop;
        [SerializeField] private LoopType _loopType;
        [SerializeField] private int _loopCount;
    
        public Ease GetEase()
        {
            return _ease;
        }

        public bool GetUseLoop()
        {
            return _useLoop;
        }
        
        public LoopType GetLoopType()
        {
            return _loopType;
        }
        
        public int GetLoopCount()
        {
            return _loopCount;
        }
        
        public float GetDuration()
        {
            return _duration;
        }
    }
}

