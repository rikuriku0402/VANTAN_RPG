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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController._playerPos = _playerController.transform.position;
            switch (ID) {

                case 0:
                    SceneManager.LoadScene("OikawaScene");
                    break;
                case 1:
                    SceneManager.LoadScene("VelScene");
                    break;
                case 2:
                    break;
                case 3:
                    break;



            };


                
            
        }
    }

}
