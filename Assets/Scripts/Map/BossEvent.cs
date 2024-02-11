using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossEvent : MonoBehaviour
{
    [SerializeField]
    int ID;
    [SerializeField]
    PlayerController _playerController;

    [SerializeField]
    private SceneLoader _sceneLoader;


    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController._playerPos = _playerController.transform.position;
            switch (ID) {

                case 0:
                    await _sceneLoader.FadeIn(SceneLoader.SceneName.OikawaScene);
                    break;
                case 1:
                    await _sceneLoader.FadeIn(SceneLoader.SceneName.VelScene);
                    break;
                case 2:
                    await _sceneLoader.FadeIn(SceneLoader.SceneName.abeScene);
                    break;
                    
                case 3:
                    break;



            };


                
            
        }
    }

}
