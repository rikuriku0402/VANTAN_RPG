using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsGame => _isGame;
    
    [SerializeField]
    [Header("SceneLoader")]
    private SceneLoader _sceneLoader;
    
    private bool _isGame = true;// Falseならゲームオーバー

    private void Start()
    {
        _sceneLoader.FadeOut();
    }

    private async void Update()
    {
        await _sceneLoader.FadeIn(SceneLoader.SceneName.Title);
        MapSceneMove._playerMapPosX = 0;
        MapSceneMove._playerMapPosY = 0;
    }

    public async void GameClear()
    {
        if (_isGame)
        {
            _isGame = false;
            await _sceneLoader.FadeIn(SceneLoader.SceneName.GameClear);
            Debug.Log("クリア");
        }
    }

    public async void GameOver()
    {
        if (!_isGame)
        {
            _isGame = true;
            await _sceneLoader.FadeIn(SceneLoader.SceneName.GameOver);
            Debug.Log("ゲームオーバー");
        }
    }
}
