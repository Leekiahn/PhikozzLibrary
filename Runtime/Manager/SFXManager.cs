using PhikozzLibrary;
using UnityEngine;


public class SFXManager : GenericSingleton<SFXManager>
{
    #region >--------------------------------------------- fields & Properties

    private AudioSource _audioSource;

    #endregion
    
    #region >--------------------------------------------- Unity

    private void Awake()
    {
        base.Awake();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
    }

    #endregion

    #region >--------------------------------------------- Play

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        _audioSource.PlayOneShot(clip, volume);
    }

    #endregion
}
