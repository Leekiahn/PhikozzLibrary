using UnityEngine;

namespace PhikozzLibrary
{
    public interface IAudioService 
    {
        void PlayBGM(AudioClip clip, bool loop = true);
        void PlaySFX(AudioClip clip);
        void StopBGM();
    }
}
