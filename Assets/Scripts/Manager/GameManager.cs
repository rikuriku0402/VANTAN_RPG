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
    
    public void ChangeGameMode(bool isGame) => _isGame = isGame;
}
