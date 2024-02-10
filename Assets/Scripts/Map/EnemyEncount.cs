using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncount : MonoBehaviour
{
    [SerializeField]
    [Header("�V�[�����[�_�[")]
    private SceneLoader _sceneLoader;

    [SerializeField]
    SceneLoader.SceneName _sceneName;

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("�G�ɓ�������");
           await _sceneLoader.FadeIn(_sceneName);
        }
    }
}

