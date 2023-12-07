using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TextView : MonoBehaviour,IPointerClickHandler
{
    public bool IsPlaying => _isPlaying;

    /// <summary>喋っている人の名前</summary>
    [SerializeField] 
    [Header("喋っている人の名前")]
    private Text _nameText = null;

    /// <summary>喋っている内容やナレーション</summary>
    [SerializeField]
    [Header("喋っている内容やナレーション")]
    private Text _talkText = null;

    [SerializeField] 
    [Header("テキストが再生しているか")]
    private bool _isPlaying = false;

    /// <summary>テキストの表示速度</summary>
    [SerializeField]
    [Range(0.1f, 0.8f)]
    [Header("テキストの表示速度")]
    private float textSpeed = 0.1f;

    /// <summary>テキストの表示時間</summary>
    [SerializeField]
    [Range(1.0f, 3.0f)]
    [Header("テキストの切り替え速度")] 
    private float _textDisplayTime = 3.0f;
    
    [SerializeField] 
    private ScenarioManager _scenarioManager;

    [SerializeField]
    private ScenarioData _scenarioData;
    
    private bool _isAuto = false;
    private bool _isClick = false;
    
    private const int NAME_LINE = 0;
    private const int TALK_LINE = 1;

    private async void Start()
    {
        _scenarioData.LineNum
            .Skip(1)
            .Subscribe(CoText).AddTo(this);

        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        CoText(_scenarioData.LineNum.Value);
    }
    
    /// <summary>GSSを上から一行ずつ出力</summary>
    private async void CoText(int value)
    {
        var data = _scenarioManager.Datas;
        
        if (data.Length == value)
            DrawText("","");
        else
        {
            Debug.Log($"現在：{value}行");
            DrawText(data[value][NAME_LINE], data[value][TALK_LINE]);
            if(!_isAuto) return;
            await UniTask.Delay(TimeSpan.FromSeconds(_textDisplayTime));
            _scenarioData.NextLine();   
        }
    }

    /// <summary>GSSの名前とセリフをテキストに反映</summary>
    private void DrawText(string name, string text)
    {
        _nameText.text = name;
        // StartCoroutine(CoDrawText(text));
        _talkText.text = text;
    }
    
    /// <summary>次の行へ</summary>
    private void Next()
    {
        // if (IsClicked())
            _scenarioData.NextLine();
            _isClick = false;
    }

    /// <summary>テキストがヌルヌル出てくるためのコルーチン</summary>
    IEnumerator CoDrawText(string text)
    {
        _isPlaying = true;
        float time = 0;
    
        while (true)
        {
            yield return null;
            time += Time.deltaTime;
    
            if (IsClicked()) break;
    
            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            _talkText.text = text.Substring(0, len);
        }
    
        _talkText.text = text;
        yield return null;
        _isPlaying = false;
    }

    /// <summary>クリック判定</summary>
    /// <returns>押されたかどうか</returns>
    public bool IsClicked()
    {
        return _isClick;
    }

    /// <summary>クリックされると一気に表示</summary>
    private IEnumerator Skip()
    {
        while (_isPlaying)
            yield return null;

        while (!IsClicked())
            yield return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isClick = true;
        Next();
    }
}
