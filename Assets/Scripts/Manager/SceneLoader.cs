using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneName SceneNames => _sceneName; 
    
    [SerializeField]
    [Header("CanvasGroup")]
    private CanvasGroup _canvasGroup;
    
    [SerializeField]
    [Header("間隔")]
    private float _duration;
    
    private SceneName _sceneName;
    
    private Dictionary<SceneName, string> _typeToName = new()
    {
        [SceneName.Unknown] = "",
        [SceneName.Title] = "Title",
        [SceneName.syokimap] = "syokimap",
        [SceneName.Battle_Oikawa] = "Battle_Oikawa",
        [SceneName.Battle_Veru] = "Battle_Veru",
        [SceneName.Battle_Zako] = "Battle_Zako",
        [SceneName.GameOver] = "GameOver",
        [SceneName.GameClear] = "GameClear",

        [SceneName.Oikawa] = "Oikawa",
        [SceneName.Est] = "Est",
        [SceneName.Zyosi] = "Zyosi",
        [SceneName.Ippan] = "Ippan",
        [SceneName.Abe] = "Abe",
        [SceneName.abeScene1]= "abeScene1",
        [SceneName.VelScene1] = "VelScene1",
        [SceneName.OikawaScene1] = "OikawaScene1",
        [SceneName.OikawaScene] = "OikawaScene",
        [SceneName.VelScene] = "VelScene",
        [SceneName.abeScene] = "abeScene",
    };

    private void Start()
    {
        _canvasGroup.alpha = 1.0f;
    }

    public enum SceneName
    {
        Unknown,
        Title,
        syokimap,
        Battle,
        Battle_Veru,
        Battle_Oikawa,
        Battle_Zako,
        GameOver,
        GameClear,
        Oikawa,
        Est,
        Zyosi,
        Ippan,
        Abe,
        abeScene1,
        VelScene1,
        OikawaScene1,
        VelScene,
        OikawaScene,
        abeScene,

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
        SceneManager.LoadSceneAsync(_typeToName[typeName]);
    }
}
