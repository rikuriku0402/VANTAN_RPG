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
    
    private const int EVENTNAME_LINE = 2;
    private const int EVENTCONTENT_LINE = 3;
    
    private async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
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
            if (EventAction.BRANCH.ToString() == eventContent)
            {
                Debug.Log("分岐を発生");
                if (t.EventName == eventName)
                {
                    Debug.Log($"{t.SheetName}の{t.LaneNum}行目へ");
                    //await UniTask.WaitUntil(() => _textView.IsClicked(), cancellationToken: token);
                    await UniTask.WaitUntil(() => !_textView.IsPlaying, cancellationToken: token);
                    await StartCoroutine(_gssReader.GetFromWeb(t.SheetName));
                    _textView.ChangeLine(t.LaneNum);
                }
            }

            if (EventAction.EFFECT.ToString() == eventContent)
            {
                Debug.Log(t.Effect);
                Debug.Log("エフェクトを生成");
            }

            if (EventAction.SE.ToString() == eventContent)
            {
                Debug.Log("効果音を再生");
                Debug.Log(t.Se);
            }
            
            if (EventAction.BGM.ToString() == eventContent)
            {
                Debug.Log("背景音楽を再生");
                Debug.Log(t.BGM);
            }
        }
    }

}
