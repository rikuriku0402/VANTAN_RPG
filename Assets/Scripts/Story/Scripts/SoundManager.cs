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

    /// <summary>���݂�Audio��ύX</summary>
    /// <param name="bgm">�ύX������Audio</param>
    /// <param name="isLoop">���[�v�Đ����邩�ǂ���</param>
    public void ChangeAudio(AudioClip bgm, bool isLoop)
    {
        _audioSource.clip = bgm;
        _audioSource.loop = isLoop;
    }

    /// <summary>Audio�𗬂�</summary>
    public void PlayAudio() => _audioSource.Play();

    /// <summary>����Ă�Audio���~�߂�</summary>
    public void StopAudio() => _audioSource.Stop();

    /// <summary>����Ă�Audio���ꎞ��~������</summary>
    public void PauseAudio() => _audioSource.Pause();

    /// <summary>Audio����x�����Đ�����</summary>
    /// <param name="se">�Đ�������Audio</param>
    public void PlaySeAudio(AudioClip se) => _audioSource.PlayOneShot(se);
}
