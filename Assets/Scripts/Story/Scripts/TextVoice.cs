using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TextVoice : MonoBehaviour
{
    [SerializeField]
    private GSSReader _gssReader;

    [SerializeField]
    private TextView _textView;

    [SerializeField]
    private SoundManager _soundManager;

    [SerializeField]
    private CharacterVoice _charaVoice;

    private const int NAME_LINE = 0;

    [SerializeField]
    private int[] _voiceID;

    private Dictionary<CharacterName, string> _charaName = new()
    {
        [CharacterName.NONE] = "",
        [CharacterName.NARRATION] = "ナレーション",
        [CharacterName.RIKU] = "りく",
        [CharacterName.VERU] = "ヴェル",
        [CharacterName.GOD] = "神様",
        [CharacterName.DAEMONKING] = "魔王",
    };

    private async void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();

        _textView.LineNum
            .Skip(1)
            .Subscribe(CoVoice)
        .AddTo(this);

        await UniTask.WaitUntil(() => !_gssReader.IsLoading, cancellationToken: token);
        CoVoice(_textView.LineNum.Value);
    }



    private void CoVoice(int value)
    {
        _soundManager.StopAudio();

        var data = _gssReader.Datas;
        if (data.Length == value) return;

        PlayVoice(data[value][NAME_LINE]);
    }


    private void PlayVoice(string name)
    {
        foreach (var t in _charaVoice.CharaVoice)
        {
            if (t.Voice== null) return;

            if (_charaName[t.CharacterName] == name)
            {
                _soundManager.PlaySeAudio(t.Voice[_voiceID[(int)t.CharacterName]]);

                _voiceID[(int)t.CharacterName]++;
            }
        }
    }
}
