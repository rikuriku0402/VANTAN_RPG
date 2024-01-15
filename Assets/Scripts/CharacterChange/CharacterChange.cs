using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChange : MonoBehaviour
{
    [SerializeField]
    [Header("キャラクターイメージ")]
    private Image _characterImage;
    
    [SerializeField]
    [Header("キャラクター一覧")]
    private List<Sprite> _characterList;
    
    [SerializeField]
    [Header("キャラクター変化ボタン")]
    private List<Button> _charaChangeButton;

    private const int LINE = 0;
    
    void Start()
    {
        int charaNum = 0;
        
        _characterImage.sprite = _characterList[charaNum];
        for (int i = 0; i < _charaChangeButton.Count; i++)
        {
            var i1 = i;
            _charaChangeButton[i].onClick.AddListener(() => StatusChange(i1));
            _charaChangeButton[i].onClick.AddListener(() => CharaChange(i1));
        }
    }

    
    private void StatusChange(int charaNum)
    {
        Debug.Log("Status" + charaNum);
        // _characterStatus[charaNum].name = /*読み取ったデータの変数*/[charaNum][/*名前の欄*/];
        // _characterStatus[charaNum].hp = /*読み取ったデータの変数*/[charaNum][/*名前の欄*/];
        // _characterStatus[charaNum].attack = 
        // _characterStatus[charaNum].defense = 
        // _characterStatus[charaNum].magicAttack = 
        // _characterStatus[charaNum].magicDefense = 
        // _characterStatus[charaNum].speed = 
        // _characterStatus[charaNum].type = CharacterStatus.Type.FIRE;
    }

    private void CharaChange(int charaNum)
    {
        Debug.Log("Character" + charaNum);
        _characterImage.sprite = _characterList[charaNum];
    }
}
