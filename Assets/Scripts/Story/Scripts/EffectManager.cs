using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    /// <summary>エフェクトを表示させる</summary>
    /// <param name="particleSystem">表示したいエフェクト</param>
    /// <param name="pos">表示したい場所</param>
    /// <param name="isLoop">ループ再生するかどうか</param>
    public void PlayEffect(ParticleSystem particleSystem, Transform pos, bool isLoop)
    {
        var effect = Instantiate(particleSystem, pos);
        effect.loop = isLoop;
        effect.Play();
    }
}
