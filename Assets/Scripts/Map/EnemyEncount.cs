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
        int rnd = UnityEngine.Random.Range(1, 11); // �� 1�`9�͈̔͂Ń����_���Ȑ����l���Ԃ�
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
            Debug.Log("�e�X�g");
            int rnd = UnityEngine.Random.Range(1, 11);
            if(rnd == 7)
            {
                //�G�ɂ����鏈��
            }
            
        }
    }

    private async UniTask Wait(int time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        // ���s���������� await Wait(5);

    }
}

