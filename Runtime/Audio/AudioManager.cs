using System;
using UnityEngine;

namespace PhikozzLibrary
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonGlobal<AudioManager>, IAudioService, IInitializable
    {
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;

        public bool IsPlayingBGM => bgmSource.isPlaying;

        public bool Init()
        {
            try
            {
                ServiceLocator.Register<IAudioService>(this);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return false;
            }
        }



        public void PlayBGM(AudioClip clip, bool loop = true)
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void StopBGM()
        {
            bgmSource.Stop();
            bgmSource.clip = null;
        }

        public void PauseBGM()
        {
            bgmSource.Pause();
        }

        public void ResumeBGM()
        {
            bgmSource.UnPause();
        }

        public void SetBGMVolume(float volume = 100f)
        {
            volume /= 100f;
            bgmSource.volume = Mathf.Clamp01(volume);
        }

        public void SetSFXVolume(float volume = 100f)
        {
            volume /= 100f;
            sfxSource.volume = Mathf.Clamp01(volume);
        }

        public void MuteBGM(bool mute)
        {
            bgmSource.mute = mute;
        }

        public void MuteSFX(bool mute)
        {
            sfxSource.mute = mute;
        }
    }
}