using UnityEngine;
using PhikozzLibrary;
using Cysharp.Threading.Tasks;

public class AudioManager : SingletonGlobal<AudioManager>, IAudioService, IPreinitialize
{
    private AudioSource _audioSource;

    public UniTask<bool> InitAsync()
    {
        try
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null) throw new System.Exception("AudioSource 컴포넌트가 프리팹에 없습니다.");
            ServiceLocator.Register<IAudioService>(this);
            return UniTask.FromResult(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("저장 시스템 초기화 실패: " + ex.Message);
            return UniTask.FromResult(false);
        }
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