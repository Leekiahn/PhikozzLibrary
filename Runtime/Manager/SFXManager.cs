using PhikozzLibrary;
using UnityEngine;

/// <summary>
/// 효과음 (SFX) 관리를 위한 매니저
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SFXManager : GenericSingleton<SFXManager>
{
    #region >--------------------------------------------- fields & Properties

    private AudioSource _audioSource;

    #endregion
    
    #region >--------------------------------------------- Unity

    protected override void Awake()
    {
        base.Awake();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    #endregion

    #region >--------------------------------------------- Play

    /// <summary>
    /// 효과음 재생
    /// </summary>
    /// <param name="clip">리소스 매니저를 통해 불러온 AudioClip</param>
    /// <param name="volume">볼륨</param>
    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        _audioSource.PlayOneShot(clip, volume);
    }

    #endregion
}
