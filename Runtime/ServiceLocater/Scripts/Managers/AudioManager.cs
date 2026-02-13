using UnityEngine;
using PhikozzLibrary.Runtime.ServiceLocater;

public class AudioManager : GenericSingleton<AudioManager>, IAudioService
{
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        // ServiceLocator에 자신을 등록
        ServiceLocator.Register<IAudioService>(this);
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
