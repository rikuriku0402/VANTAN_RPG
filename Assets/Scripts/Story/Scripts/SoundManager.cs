using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>現在のAudioを変更</summary>
    /// <param name="bgm">変更したいAudio</param>
    /// <param name="isLoop">ループ再生するかどうか</param>
    public void ChangeAudio(AudioClip bgm, bool isLoop)
    {
        _audioSource.clip = bgm;
        _audioSource.loop = isLoop;
    }

    /// <summary>Audioを流す</summary>
    public void PlayAudio() => _audioSource.Play();

    /// <summary>流れてるAudioを止める</summary>
    public void StopAudio() => _audioSource.Stop();

    /// <summary>流れてるAudioを一時停止させる</summary>
    public void PauseAudio() => _audioSource.Pause();

    /// <summary>Audioを一度だけ再生する</summary>
    /// <param name="se">再生したいAudio</param>
    public void PlaySeAudio(AudioClip se) => _audioSource.PlayOneShot(se);
}
