using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "StoryEventData", menuName = "ScriptableObjects/StoryEventData")] 
public class StoryEventData : ScriptableObject
{
    public List<StoryEvent> Event => _event;

    [SerializeField]
    private List<StoryEvent> _event = new();
}

[System.Serializable]
public class StoryEvent
{
    public string EventName => _eventName;
    public string SheetName => _sheetName;
    public int LaneNum => _laneNum;
    public ParticleSystem Effect => _effect;
    public AudioClip Se => _se;
    public AudioSource BGM => _bgm;
    public EventAction Action => _eventAction;
    
    [SerializeField]
    [Header("何がしたいか")]
    private EventAction _eventAction;
    
    [SerializeField] 
    [Header("イベント名")]
    private string _eventName;

    [SerializeField]
    [Header("読みこむシート名")]
    private string _sheetName;
    
    [SerializeField]
    [Header("読みこむ行数")]
    private int _laneNum = 1; 

    [SerializeField] 
    [Header("生成したいエフェクト")]
    private ParticleSystem _effect;

    [SerializeField] 
    [Header("再生したいSE")]
    private AudioClip _se;
    
    [SerializeField] 
    [Header("再生したいBGM")] 
    private AudioSource _bgm;

}

public enum EventAction
{
    BRANCH,
    EFFECT,
    SE,
    BGM,
}
