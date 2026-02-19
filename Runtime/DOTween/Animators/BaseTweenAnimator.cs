using UnityEngine;
using System;

namespace PhikozzLibrary
{
    [Serializable]
    public class TweenData
    {
        public string key;
        public BaseTweenPreset tweenPreset;
    }

    public abstract class BaseTweenAnimator : MonoBehaviour
    {
        public abstract void Play(string key);
    }
}

