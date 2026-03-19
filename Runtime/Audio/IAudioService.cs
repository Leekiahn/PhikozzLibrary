using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PhikozzLibrary
{
    public interface IAudioService 
    {
        bool IsPlayingBGM { get; }

        void PlayBGM(AudioClip clip, bool loop = true);
        void PlaySFX(AudioClip clip);
        void StopBGM();

        void PauseBGM();
        void ResumeBGM();

        void SetBGMVolume(float volume);
        void SetSFXVolume(float volume);

        void MuteBGM(bool mute);
        void MuteSFX(bool mute);
    }
}
