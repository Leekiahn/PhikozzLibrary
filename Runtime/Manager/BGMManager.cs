using PhikozzLibrary;
using UnityEngine;

/// <summary>
/// 길이가 긴 배경음악(BGM) 관리를 위한 매니저
/// </summary>
public class BGMManager : GenericSingleton<BGMManager>
{
    #region >--------------------------------------------- fields & Properties

    private static AudioSource _audioSource;

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

    /// <summary>
    /// BGM 재생
    /// </summary>
    /// <param name="clip">리소스매니저를 통해 불러온 AudioClip</param>
    /// <param name="volume">볼륨</param>
    public static void PlayBGM(AudioClip clip, float volume = 1.0f)
    {
        if(_audioSource.clip == null) return;
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.Play();
    }
    
    /// <summary>
    /// BGM 정지
    /// </summary>
    public static void StopBGM()
    {
        _audioSource.Stop();
    }

    #endregion
}
