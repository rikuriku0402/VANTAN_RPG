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
    
    private bool _isGame;// Falseならゲームオーバー

    private void Start()
    {
        _sceneLoader.FadeOut();
    }

    public async void GameClear()
    {
        _isGame = true;
        
        if (_isGame)
        {
            await _sceneLoader.FadeIn(SceneLoader.SceneName.GameClear);
        }
    }

    public async void GameOver()
    {
        _isGame = false;

        if (!_isGame)
        {
            await _sceneLoader.FadeIn(SceneLoader.SceneName.GameOver);
        }
    }
    
    public bool GameModeChange(bool isGame) => _isGame = isGame;
}
