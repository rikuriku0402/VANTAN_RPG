using UnityEngine;
using System;
using UniRx;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class SaveData
{
    public string name;
    public int hp;
    public int atk;
    public int def;
    public int magicAtk;
    public int magicDef;
    public int speed;
    public string type;
}

public class CharacterStatusData : MonoBehaviour
{
    public IReadOnlyList<SaveData> SaveData => _saveData; 

    [SerializeField]
    private List<SaveData> _saveData;

    public void SetGameDataValue(SaveData saveData, int num)
    {
        _saveData.Add(saveData);
    }    
}
