using UnityEngine;
using System;
using UniRx;

[Serializable]
public class SaveData
{
    public IntReactiveProperty _lineNum;
    public string _name;
    public string _talk;
}

public class ScenarioData : MonoBehaviour
{
    public IntReactiveProperty LineNum => _lineNum;
    public string Name => _name;
    public string Talk => _talk;
    
    [SerializeField]
    [Header("現在の行数")]
    private IntReactiveProperty _lineNum = new(1);
    
    [SerializeField]
    [Header("名前")]
    private string _name;
    
    [SerializeField]
    [Header("台詞")]
    private string _talk;

    public void SetGameDataValue(SaveData saveData)
    {
        ChangeLine(saveData._lineNum.Value);
        _name = saveData._name;
        _talk = saveData._talk;
    }

    public void NextLine() => _lineNum.Value++;

    public void ChangeLine(int num) => _lineNum.Value = num;
}
