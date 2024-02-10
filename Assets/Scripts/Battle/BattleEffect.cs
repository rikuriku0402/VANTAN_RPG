using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffect : MonoBehaviour
{
    [SerializeField]
    [Header("エフェクトを出す場所")]
    private Transform _enemyPos;
    
    [SerializeField]
    [Header("エフェクトを出す場所")]
    private Transform _playerPos;
    
    [SerializeField]
    [Header("攻撃エフェクト")]
    private ParticleSystem _attackEffect;
    
    [SerializeField]
    [Header("エフェクトマネージャー")]
    private EffectManager _effectManager;

    public void EnemyAttackEffectOn()
    {
        _effectManager.PlayEffect(_attackEffect, _playerPos, false);
        Debug.Log("プレイヤーに攻撃した");
    }
    
    public void PlayerAttackEffectOn()
    {
        _effectManager.PlayEffect(_attackEffect, _enemyPos, false);        
        Debug.Log("敵に攻撃した");
    }
}
