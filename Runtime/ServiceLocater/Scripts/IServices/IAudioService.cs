using UnityEngine;

namespace PhikozzLibrary.Runtime.ServiceLocater
{
    public interface IAudioService 
    {
        void PlayBGM(AudioClip clip, bool loop = true);
        void PlaySFX(AudioClip clip);
        void StopBGM();
    }
}
