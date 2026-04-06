using System;
using UnityEngine;

namespace PhikozzLibrary
{
    public class AudioManager : SingletonGlobal<AudioManager>, IAudioService, IInitializable
    {
        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioSource _sfxSource;

        public bool IsPlayingBGM => _bgmSource.isPlaying;

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
            _bgmSource.clip = clip;
            _bgmSource.loop = loop;
            _bgmSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
        }

        public void StopBGM()
        {
            _bgmSource.Stop();
            _bgmSource.clip = null;
        }

        public void PauseBGM()
        {
            _bgmSource.Pause();
        }

        public void ResumeBGM()
        {
            _bgmSource.UnPause();
        }

        public void SetBGMVolume(float volume = 100f)
        {
            volume /= 100f;
            _bgmSource.volume = Mathf.Clamp01(volume);
        }

        public void SetSFXVolume(float volume = 100f)
        {
            volume /= 100f;
            _sfxSource.volume = Mathf.Clamp01(volume);
        }

        public void MuteBGM(bool mute)
        {
            _bgmSource.mute = mute;
        }

        public void MuteSFX(bool mute)
        {
            _sfxSource.mute = mute;
        }
    }
}