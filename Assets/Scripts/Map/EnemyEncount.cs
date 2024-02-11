using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncount : MonoBehaviour
{
    [SerializeField]
    [Header("シーンローダー")]
    private SceneLoader _sceneLoader;

    [SerializeField]
    SceneLoader.SceneName _sceneName;

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            Debug.Log("敵に当たった");
          // await _sceneLoader.FadeIn(_sceneName);
        }
    }
}

