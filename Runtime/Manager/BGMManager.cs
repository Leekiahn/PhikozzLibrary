using PhikozzLibrary;
using UnityEngine;

public class BGMManager : GenericSingleton<BGMManager>
{
    #region >--------------------------------------------- fields & Properties

    private AudioSource _audioSource;

    #endregion

    #region >--------------------------------------------- Unity

    protected override void Awake()
    {
        base.Awake();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
    }

    #endregion

    #region >--------------------------------------------- Play & Stop

    public void PlayBGM(AudioClip clip, float volume = 1.0f)
    {
        if(_audioSource.clip == null) return;
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.Play();
    }
    
    public void StopBGM()
    {
        _audioSource.Stop();
    }

    #endregion
}
