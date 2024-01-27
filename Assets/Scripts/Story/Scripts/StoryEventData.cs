using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StoryEventData", menuName = "ScriptableObjects/StoryEventData")] 
public class StoryEventData : ScriptableObject
{
    public IReadOnlyList<StoryEvent> StoryEvent => _event;

    [SerializeField]
    private List<StoryEvent> _event = new();
}

[System.Serializable]
public class StoryEvent
{
    public EventName EventName => _eventName;
    public SheetName SheetName => _sheetName;
    public int LaneNum => _laneNum;
    public ParticleSystem[] Effect => _effect;
    public AudioClip SE => _se;
    public AudioClip BGM => _bgm;
    public EventAction[] Action => _eventAction;

    [SerializeField]
    private string _event;

    [SerializeField]
    [Header("イベント名")]
    private EventName _eventName;

    [SerializeField]
    [Header("読みこむシート名")]
    private SheetName _sheetName;
    
    [SerializeField]
    [Header("読みこむ行数")]
    private int _laneNum = 1;

    [SerializeField] 
    [Header("生成したいエフェクト")]
    private ParticleSystem[] _effect;

    [SerializeField] 
    [Header("再生したいSE")]
    private AudioClip _se;
    
    [SerializeField] 
    [Header("再生したいBGM")] 
    private AudioClip _bgm;

    [SerializeField]
    [Header("何がしたいか")]
    private EventAction[] _eventAction;

}

public enum EventAction
{
    NONE,
    BRANCH,
    EFFECT,
    SE,
    PLAY_BGM,
    STOP_BGM,
    PAUSE_BGM,
}

public enum EventName
{
    BRANCH_HIGHSCHOOL,
    BRANCH_BATLLE,
    BRANCH_EST,
    BRANCH_GIRL,
    BRANCH_OIKAWA,
    HIGHSCHOOL_BGM,
    EST_BGM,
    GIRL_BGM,
    OIKAWA_BGM
}

public enum SheetName
{
    NONE,
    SHEET_A,
    SHEET_B,
    SHEET_BATLLE,
    SHEET_EST,
    SHEET_HIGHSCHOOL,
    SHEET_GIRL,
    SHEET_OIKAWA,
}
