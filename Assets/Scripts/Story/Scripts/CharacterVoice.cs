using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "CharaVoiceData", menuName = "ScriptableObjects/CharaVoiceData")]
public class CharacterVoice : ScriptableObject
{
    public IReadOnlyList<CharaVoice> CharaVoice => _charaVoice;

    [SerializeField]
    [Header("�L�����N�^�[�ʉ����ꗗ")]
    private List<CharaVoice> _charaVoice = new();
}

[System.Serializable]
public class CharaVoice
{
    public CharacterName CharacterName => _charaName;
    public int VoiceID => _voiceID;
    public AudioClip[] Voice => _voice;

    [SerializeField]
    [Header("�L������")]
    private string _name;

    [SerializeField]
    [Header("�N�̉����ꗗ��")]
    private CharacterName _charaName;

    [Header("���ڂ̃{�C�X���炩")]
    private int _voiceID;

    [SerializeField]
    [Header("�����i�V�i���I���j")]
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

