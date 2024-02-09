using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Cysharp.Threading.Tasks;
    

public class EnemyEncount : MonoBehaviour
{
    public bool _konaideRock = false;

    void Start()
    {
        int rnd = UnityEngine.Random.Range(1, 11); // ※ 1～9の範囲でランダムな整数値が返る
    }

    private void FixedUpdate()
    {
        Encount();
    }

    private void Encount()
    {
        
    }

    private async void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"&& _konaideRock==false)
        {
            Debug.Log("テスト");
            int rnd = UnityEngine.Random.Range(1, 11);
            if(rnd == 7)
            {
                //敵にあたる処理
            }
            
        }
    }

    private async UniTask Wait(int time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        // 実行したいもの await Wait(5);

    }
}

