using UnityEngine;
using PhikozzLibrary;

public class AudioManager : SingletonGlobal<AudioManager>, IAudioService
{
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
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
