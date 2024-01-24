using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapSceneMove : MonoBehaviour
{
    // Start is called before the first frame update
    public static int _playerMapPosX = 0;
    public static int _playerMapPosY =0;

    [SerializeField]
    Exit_TYPE _type;

    [SerializeField]
    private SceneLoader _sceneLoader;
    [SerializeField] enum Exit_TYPE
    {
        UP,
        RIGHT,
        UNDER,
        LEFT,
    }

    [SerializeField] SceneBase _scenebase;

    private void Start()
    {
       // Debug.Log(_sceneData.Name[1]);
       
   
    }

    public async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (_type)
            {
                case Exit_TYPE.UP:
                    _playerMapPosY += 1;
                    Debug.Log("‚¤‚¦");
                    break;
                case Exit_TYPE.RIGHT:
                    _playerMapPosX += 1;
                    Debug.Log("‚Ý‚¬");
                    break;
                case Exit_TYPE.UNDER:
                    Debug.Log("‚µ‚½");
                    _playerMapPosY -= 1;
                    break;
                case Exit_TYPE.LEFT:
                    Debug.Log("‚Ð‚¾‚è");
                    _playerMapPosX -= 1;
                    Debug.Log("x‚Í" + _playerMapPosX + "Y‚Í" + _playerMapPosY);
                    break;
            };

            foreach (var t in _scenebase.ST_scenebase)
            {
                if (t.Xpoint != _playerMapPosX) continue;
                if (t.Ypoint != _playerMapPosY) continue;
                Debug.Log(t.Test);

                await _sceneLoader.FadeIn(t.Test);
               
            }


        }

    }
}
