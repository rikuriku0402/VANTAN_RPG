using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Cysharp.Threading.Tasks;
public class curiaTitle : MonoBehaviour
{

    [SerializeField]
    private SceneLoader _sceneLoader;
    // Start is called before the first frame update
    async void Start()
    {
        await Wait(5);
    }
    private async UniTask Wait(int time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        MapSceneMove._playerMapPosX = 0;
        MapSceneMove._playerMapPosY = 0;
        await _sceneLoader.FadeIn(SceneLoader.SceneName.Title);
        
    }
}
