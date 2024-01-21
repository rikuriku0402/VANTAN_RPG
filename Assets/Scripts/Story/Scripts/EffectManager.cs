using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    /// <summary>�G�t�F�N�g��\��������</summary>
    /// <param name="particleSystem">�\���������G�t�F�N�g</param>
    /// <param name="pos">�\���������ꏊ</param>
    /// <param name="isLoop">���[�v�Đ����邩�ǂ���</param>
    public void PlayEffect(ParticleSystem particleSystem, Transform pos, bool isLoop)
    {
        var effect = Instantiate(particleSystem, pos);
        effect.loop = isLoop;
        effect.Play();
    }
}
