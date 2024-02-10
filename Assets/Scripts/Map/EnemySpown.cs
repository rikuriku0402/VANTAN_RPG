using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Cysharp.Threading.Tasks;
public class EnemySpown : MonoBehaviour
{
    public bool _konaideRock = false;
    private async void
        OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _konaideRock == false)
        {
            Debug.Log("プレイヤーにあたった");
            int rnd = UnityEngine.Random.Range(1, 11);
            if(rnd == 7)
            {
                Debug.Log("敵にあった");
            }
        }
    }
    private async UniTask Wait(int time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        // 実行したいもの await Wait(5);

    }
}

