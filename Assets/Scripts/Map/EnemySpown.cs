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
            Debug.Log("�v���C���[�ɂ�������");
            int rnd = UnityEngine.Random.Range(1, 11);
            if(rnd == 7)
            {
                Debug.Log("�G�ɂ�����");
            }
        }
    }
    private async UniTask Wait(int time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        // ���s���������� await Wait(5);

    }
}

