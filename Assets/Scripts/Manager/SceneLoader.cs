using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    [Header("CanvasGroup")]
    private CanvasGroup _canvasGroup;
    
    [SerializeField]
    [Header("間隔")]
    private float _duration;
    
    private Dictionary<SceneName, string> _typeToName = new()
    {
        [SceneName.Unknown] = "",
        [SceneName.Title] = "Title",
        [SceneName.Game] = "Game",
        [SceneName.GameOver] = "GameOver",
        [SceneName.GameClear] = "GameClear",
    };

    private void Start()
    {
        _canvasGroup.alpha = 1.0f;
    }

    public enum SceneName
    {
        Unknown,
        Title,
        Game,
        GameOver,
        GameClear,
    }

    public async UniTask FadeIn(SceneName typeName)
    {
        _canvasGroup.FadeIn(_duration); 
        await UniTask.Delay(TimeSpan.FromSeconds(_duration));
        SceneChange(typeName);
    }
    
    public void FadeOut()
    {
        _canvasGroup.FadeOut(_duration);
    }
    
    private void SceneChange(SceneName typeName)
    {
        SceneManager.LoadScene(_typeToName[typeName]);
    }
}
