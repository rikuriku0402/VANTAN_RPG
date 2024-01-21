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
    private SoundManager _soundManager;

    [SerializeField]
    private EffectManager _effectManager;

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
        EventFlag(data[value][EVENTNAME_LINE]);
    }

    private async void EventFlag(string eventName)
    {
        var token = this.GetCancellationTokenOnDestroy();

        foreach (var t in _storyEventData.Event)
        {
            if (t.EventName != eventName) continue;

            for (int i = 0; i < t.Action.Length; i++)
            {
                if (EventAction.BRANCH == t.Action[i])
                {
                    Debug.Log($"SheetName {t.SheetName} => No.{t.LaneNum}");

                    if (!_textView.IsAuto)
                    {
                        await UniTask.WaitUntil(() => _textView.IsClicked(), cancellationToken: token);
                        await _textView.Skip();
                    }

                    await _gssReader.GetFromWeb(t.SheetName);
                    _textView.ChangeLine(t.LaneNum);
                }

                if (EventAction.EFFECT == t.Action[i])
                {
                    for (int k = 0; k < t.Effect.Length; k++)
                    {
                        Debug.Log($"Play Effect {t.Effect[k]}");
                        _effectManager.PlayEffect(t.Effect[k], this.gameObject.transform, false);
                    }
                }

                if (EventAction.SE == t.Action[i])
                {
                    Debug.Log($"Play SE {t.SE}");

                    _soundManager.PlaySeAudio(t.SE);
                }

                if (EventAction.PLAY_BGM == t.Action[i])
                {
                    Debug.Log($"Play BGM {t.BGM}");

                    _soundManager.ChangeAudio(t.BGM, true);
                    _soundManager.PlayAudio();
                }

                if (EventAction.STOP_BGM == t.Action[i])
                {
                    Debug.Log($"Stop BGM {t.BGM}");

                    _soundManager.StopAudio();
                }

                if(EventAction.PAUSE_BGM == t.Action[i])
                {
                    Debug.Log($"{t.BGM} pause");
                }
            }
        }
    }

}
