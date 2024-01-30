using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "CharaVoiceData", menuName = "ScriptableObjects/CharaVoiceData")]
public class CharacterVoice : ScriptableObject
{
    public IReadOnlyList<CharaVoice> CharaVoice => _charaVoice;

    [SerializeField]
    [Header("キャラクター別音源一覧")]
    private List<CharaVoice> _charaVoice = new();
}

[System.Serializable]
public class CharaVoice
{
    public CharacterName CharacterName => _charaName;
    public int VoiceID => _voiceID;
    public AudioClip[] Voice => _voice;

    [SerializeField]
    [Header("キャラ名")]
    private string _name;

    [SerializeField]
    [Header("誰の音源一覧か")]
    private CharacterName _charaName;

    [Header("何個目のボイスからか")]
    private int _voiceID;

    [SerializeField]
    [Header("音源（シナリオ順）")]
    private AudioClip[] _voice;

}


public enum CharacterName
{
    NONE,
    NARRATION,
    GOD,
    DAEMONKING,
    RIKU,
    VERU,

}

