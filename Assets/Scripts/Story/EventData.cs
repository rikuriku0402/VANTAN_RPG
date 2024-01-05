using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData")] 
public class EventData : ScriptableObject
{
    public List<Event> Event => _event;

    [SerializeField]
    private List<Event> _event = new();
}

[System.Serializable]
public class Event
{
    public string EventName => _eventName;
    public string SheetName => _sheetName;
    public int LaneNum => _laneNum;
    public ParticleSystem Effect => _effect;
    public AudioClip Se => _se;
    public AudioSource BGM => _bgm;
    public EventAction Action => _eventAction;
    
    [SerializeField] 
    [Header("イベント名")]
    private string _eventName;

    [SerializeField]
    [Header("読みこむシート名")]
    private string _sheetName;
    
    [SerializeField]
    [Range(1, 5)]
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
    
    [SerializeField]
    [Header("何がしたいか")]
    private EventAction _eventAction;
    
}

public enum EventAction
{
    Branch,
    Effect,
    SE,
    BGM,
}
