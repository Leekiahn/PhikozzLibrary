using UnityEngine;
using PhikozzLibrary;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class AudioManager : SingletonGlobal<AudioManager>, IAudioService, IPreinitialize
{
    private AudioSource _audioSource;

    public UniTask<bool> InitAsync()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        ServiceLocator.Register<IAudioService>(this);
        return UniTask.FromResult(true);
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (_audioSource == null || clip == null) return;
        _audioSource.clip = clip;
        _audioSource.loop = loop;
        _audioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (_audioSource == null || clip == null) return;
        _audioSource.PlayOneShot(clip);
    }

    public void StopBGM()
    {
        if (_audioSource == null) return;
        _audioSource.Stop();
    }
}
