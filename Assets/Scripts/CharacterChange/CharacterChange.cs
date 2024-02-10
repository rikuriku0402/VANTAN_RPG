using System;
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
    private List<Sprite> _characterSpriteList;
    
    [SerializeField]
    [Header("キャラクター変化ボタン")]
    private List<Button> _charaChangeButton;
    
    [SerializeField]
    [Header("キャラクター")]
    private List<GameObject> _charaList;

    private void Start()
    {
        int charaNum = 0;

        _characterImage.sprite = _characterSpriteList[charaNum];
        for (int i = 0; i < _charaChangeButton.Count; i++)
        {
            if (_charaChangeButton[i] == null) return;
            
            _charaList[i].SetActive(false);
            var i1 = i;
            _charaChangeButton[i].onClick.AddListener(() => CharaChange(i1));
        }
        
        _charaList[charaNum].SetActive(true);
    }

    private void CharaChange(int charaNum)
    {
        _charaList.ForEach(x => x.gameObject.SetActive(false));
        
        Debug.Log("Character" + charaNum);
        _charaList[charaNum].SetActive(true);
        _characterImage.sprite = _characterSpriteList[charaNum];
        
        Debug.Log(_charaList[charaNum]);
    }
}
