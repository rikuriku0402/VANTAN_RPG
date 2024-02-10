using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStart : MonoBehaviour
{
    [SerializeField]
    [Header("シーンベース")]
    private SceneBase _sceneBase;
    
    [SerializeField]
    [Header("シーンローダー")]
    private SceneLoader _sceneLoader;
    
    [SerializeField]
    [Header("背景")]
    private Image _backGround;
    
    [SerializeField]
    [Header("スプライト")]
    private List<Sprite> _backGroundSprites;
    
    void Start()
    {
        foreach (var t in _sceneBase.ST_scenebase)
        {
            if (t.Xpoint != MapSceneMove._playerMapPosX) continue;
            if (t.Ypoint != MapSceneMove._playerMapPosY) continue;
            Debug.Log(t.Test);
        }

        switch (_sceneLoader.SceneNames)
        {
            case SceneLoader.SceneName.Unknown:
            case SceneLoader.SceneName.Title:
            case SceneLoader.SceneName.Game:
            case SceneLoader.SceneName.GameOver:
            case SceneLoader.SceneName.GameClear:
                break;
            
            case SceneLoader.SceneName.Oikawa:
                _backGround.sprite = _backGroundSprites[0];
                break;
            case SceneLoader.SceneName.Est:
                _backGround.sprite = _backGroundSprites[1];
                break;
            case SceneLoader.SceneName.Zyosi:
                _backGround.sprite = _backGroundSprites[2];
                break;
            case SceneLoader.SceneName.Ippan:
                _backGround.sprite = _backGroundSprites[3];
                break;
        }
    }
}
