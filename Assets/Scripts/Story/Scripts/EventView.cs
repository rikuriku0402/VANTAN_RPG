using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class EventView : MonoBehaviour
{
    [SerializeField] 
    private GSSReader _gssReader; 
    
    [SerializeField]
    private TextView _textView;
    
    [SerializeField]
    private StoryEventData _storyEventData;

    [SerializeField]
    private AudioSource _bgmSource;

    private const int EVENTNAME_LINE = 2;
    private const int EVENTCONTENT_LINE = 3;
    
    private async void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.WaitUntil(() => !_gssReader.IsLoading, cancellationToken: token);

        _textView.LineNum
            .Skip(1)
            .Subscribe(EventCheck)
            .AddTo(this);
    }

    private void EventCheck(int value)
    {
        var data = _gssReader.Datas;
        if(data.Length == value) return;
        EventFlag(data[value][EVENTNAME_LINE], data[value][EVENTCONTENT_LINE]);
    }

    private async void EventFlag(string eventName, string eventContent)
    {
        var token = this.GetCancellationTokenOnDestroy();
        foreach (var t in _storyEventData.Event)
        {
            if (t.EventName != eventName) continue;

            for (int i = 0; i < t.Action.Length; i++)
            {
                if (EventAction.BRANCH == t.Action[i])
                {
                    Debug.Log("分岐を発生");
                    if (t.EventName == eventName)
                    {
                        Debug.Log($"{t.SheetName}の{t.LaneNum}行目へ");
                        if (!_textView.IsAuto)
                        {
                            await UniTask.WaitUntil(() => _textView.IsClicked(), cancellationToken: token);
                            await _textView.Skip();
                        }
                        await _gssReader.GetFromWeb(t.SheetName);
                        _textView.ChangeLine(t.LaneNum);
                    }
                }

                if (EventAction.EFFECT == t.Action[i])
                {
                    Debug.Log(t.Effect);
                    Debug.Log("エフェクトを生成");

                    for(int k = 0; k < t.Effect.Length; k++)
                    {
                        var effect = Instantiate(t.Effect[k], this.gameObject.transform /*座標を入力*/);
                        effect.loop = false;
                        effect.Play();
                    }

                }

                if (EventAction.SE == t.Action[i])
                {
                    Debug.Log("効果音を再生");
                    Debug.Log(t.SE);

                    _bgmSource.PlayOneShot(t.SE);
                }

                if (EventAction.PLAY_BGM == t.Action[i])
                {
                    Debug.Log("背景音楽を再生");
                    Debug.Log(t.BGM);

                    _bgmSource.clip = t.BGM;
                    _bgmSource.Play();
                }

                if (EventAction.STOP_BGM == t.Action[i])
                {
                    Debug.Log("背景音楽を再生");
                    Debug.Log(t.BGM);

                    _bgmSource.Stop();
                }
            }
        }
    }

}
