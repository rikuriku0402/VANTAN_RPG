using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class EventView : MonoBehaviour
{
    [SerializeField] 
    private ScenarioManager _scenarioManager; 
    
    [SerializeField] 
    private ScenarioData _scenarioData;

    [SerializeField]
    private TextView _textView;
    
    [SerializeField]
    private EventData _eventData;
    
    private const int EVENTNAME_LINE = 2;
    private const int EVENTCONTENT_LINE = 3;
    
    private async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); 
        _scenarioData.LineNum
            .Skip(1)
            .Subscribe(EventCheck)
            .AddTo(this);
    }

    private void EventCheck(int value)
    {
        var data = _scenarioManager.Datas;
        OnEvent(data[value][EVENTNAME_LINE], data[value][EVENTCONTENT_LINE]);
    }

    private async void OnEvent(string eventName, string eventContent)
    {
        var token = this.GetCancellationTokenOnDestroy();
        foreach (var t in _eventData.Event)
        {
            if (t.EventName != eventName) continue;
            if (EventAction.Branch.ToString() == eventContent)
            {
                Debug.Log("分岐を発生");
                if (t.EventName == eventName)
                {
                    Debug.Log($"{t.SheetName}の{t.LaneNum}行目へ");
                    await StartCoroutine(_scenarioManager.GetFromWeb(t.SheetName));
                    await UniTask.WaitUntil(() => _textView.IsClicked(), cancellationToken: token);
                    _scenarioData.ChangeLine(t.LaneNum);
                }
            }

            if (EventAction.Effect.ToString() == eventContent)
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
