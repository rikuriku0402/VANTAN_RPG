using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Unity.VisualScripting.Antlr3.Runtime;
using System.Threading.Tasks;

public class TextView : MonoBehaviour
{
    public bool IsPlaying => _isPlaying;
    public bool IsAuto => _isAuto;
    public IntReactiveProperty LineNum => _lineNum;

    [SerializeField]
    [Header("現在の行数")]
    private IntReactiveProperty _lineNum;

    /// <summary>喋っている人の名前</summary>
    [SerializeField] 
    [Header("喋っている人の名前")]
    private Text _nameText = null;

    /// <summary>喋っている内容やナレーション</summary>
    [SerializeField]
    [Header("喋っている内容やナレーション")]
    private Text _talkText = null;

    [SerializeField] 
    [Header("クリックできるか")]
    private bool _clickable = true;

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
    private GSSReader _gssReader;

    [SerializeField]
    private bool _isAuto = false;

    [SerializeField]
    private bool _isPlaying = false;
    
    private const int NAME_LINE = 0;
    private const int TALK_LINE = 1;

    private async void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();

        _lineNum
            .Skip(1)
            .Subscribe(CoText).AddTo(this);

        //this.UpdateAsObservable()
        //    .Subscribe(_ => Next())
        //    .AddTo(this);

        await UniTask.WaitUntil(() => !_gssReader.IsLoading, cancellationToken: token);
        CoText(_lineNum.Value);
    }
    
    /// <summary>GSSを上から一行ずつ出力</summary>
    public async void CoText(int value)
{
        var data = _gssReader.Datas;
        if(data.Length == value) return;
        Debug.Log($"{value}行目");

        DrawText(data[value][NAME_LINE], data[value][TALK_LINE]);
        if (!_isAuto) await Skip();
        else await UniTask.Delay(TimeSpan.FromSeconds(_textDisplayTime));

        NextLine();
    }

    /// <summary>GSSの名前とセリフをテキストに反映</summary>
    private void DrawText(string name, string text)
    {
        _nameText.text = name;
        StartCoroutine(CoDrawText(text));
        //_talkText.text = text;
    }

    /// <summary>テキストがヌルヌル出てくるためのコルーチン</summary>
    private IEnumerator CoDrawText(string text)
    {
        _isPlaying = true;
        float time = 0;

        while (true)
        {
            yield return null;
            time += Time.deltaTime;

            if (IsClicked() && _clickable) break;

            var len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            _talkText.text = text.Substring(0, len);
        }

        _talkText.text = text;
        yield return null;
        _isPlaying = false;
    }

    //private void Next()
    //{
    //    if (IsClicked() && _clickable) NextLine();
    //}

    /// <summary>クリック判定</summary>
    /// <returns>押されたかどうか</returns>
    public bool IsClicked()
    {
        return Input.GetMouseButtonDown(0);
    }

    /// <summary>クリックされると一気に表示</summary>
    public IEnumerator Skip()
    {
        while (_isPlaying) yield return null;

        while (!IsClicked()) yield return null;

        while (!_clickable) yield return null;
    }

    /// <summary>次の行へ</summary>
    private void NextLine() => _lineNum.Value++;

    /// <summary>
    /// 指定された行数へ
    /// </summary>
    /// <param name="num">行数</param>
    public void ChangeLine(int num) => _lineNum.Value = num;
}
