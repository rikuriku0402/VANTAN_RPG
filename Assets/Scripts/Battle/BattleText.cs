using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleText : MonoBehaviour
{
    [SerializeField]
    [Header("バトルログ")]
    private Text _battleLogText;
    
    public void BattleLog(string name,string enemyName, int damage)
    {
        _battleLogText.text = name + "が" + enemyName + "に" + damage + "与えた";
    }
}
