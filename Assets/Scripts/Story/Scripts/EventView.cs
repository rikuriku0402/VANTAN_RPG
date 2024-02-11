using System;
using System.Collections.Generic;
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

    [SerializeField]
    private SceneLoader _sceneLoader;

    [SerializeField]
    private int _rikuVoiceCount;

    private Dictionary<SheetName, string> _sheetName = new()
    {
        [SheetName.NONE] = null,
        [SheetName.SHEET_A] = "�n�܂�̏�",
        [SheetName.SHEET_B] = "B���[�g",
        [SheetName.SHEET_BATLLE] = "�o�g���V�[��",
        [SheetName.SHEET_GIRL] = "���q�}�b�v",
        [SheetName.SHEET_EST] = "EST�}�b�v",
        [SheetName.SHEET_HIGHSCHOOL] = "��ʍZ�}�b�v",
        [SheetName.SHEET_OIKAWA] = "�y��}�b�v",
    };

    private Dictionary<EventName, string> _eventName = new()
    {
        [EventName.BRANCH_HIGHSCHOOL] = "�x���̑�w����",
        [EventName.BRANCH_EST] = "VANTANT�̃��r�[",
        [EventName.BRANCH_GIRL] = "�ߋ��̕���",
        [EventName.BRANCH_OIKAWA] = "�y��̃G���A",
        [EventName.HIGHSCHOOL_BGM] = "��ʍZ�}�b�vBGM",
        [EventName.EST_BGM] = "EST�}�b�vBGM",
        [EventName.GIRL_BGM] = "���q�}�b�vBGM",
        [EventName.OIKAWA_BGM] = "�y��}�b�vBGM",
        [EventName.CHANGE_HIGHSCHOOL] = "�x���퓬�J�n",
        [EventName.CHANGE_OIKAWA] = "��������퓬�J�n",
        [EventName.RETURN_HIGHSCHOOLMAP] = "b",
    };


    private const int NAME_LINE = 0;

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
        if (data.Length == value) return;
        EventFlag(data[value][EVENTNAME_LINE]);
    }

    private async void EventFlag(string eventName)
    {
        var token = this.GetCancellationTokenOnDestroy();

        foreach (var t in _storyEventData.StoryEvent)
        {
            if (_eventName[t.EventName] != eventName) continue;

            for (int i = 0; i < t.Action.Length; i++)
            {
                if (EventAction.BRANCH == t.Action[i])
                {
                    Debug.Log($"SheetName {_sheetName[t.SheetName]} => No.{t.LaneNum}");

                    if (!_textView.IsAuto)
                    {
                        await UniTask.WaitUntil(() => _textView.IsClicked(), cancellationToken: token);
                        await _textView.Skip();
                    }

                    await _gssReader.GetFromWeb(_sheetName[t.SheetName]);
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

                if (EventAction.PAUSE_BGM == t.Action[i])
                {
                    Debug.Log($"Pause BGM {t.BGM}");
                }

                if(EventAction.CHANGE_SCENE == t.Action[i])
                {
                    if(_eventName[t.EventName] == "")
                    {

                    }
                    //_sceneLoader.FadeIn(SceneLoader.SceneName.Title/*�o�g���V�[���˓�*/);
                }

                if (EventAction.NONE == t.Action[i])
                {
                    Debug.Log("");
                }
            }
        }
    }
}
