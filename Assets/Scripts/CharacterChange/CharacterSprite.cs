using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharacterSprite : MonoBehaviour
{
    [SerializeField]
    [Header("キャラクターイメージ")]
    private Image _image;
    
    [SerializeField]
    [Header("キャラクターリスト")]
    private List<Sprite> _charaList;
    
    
    public void EnemyImageRoad(int charaNum)
    { 
        _image.sprite = _charaList[charaNum];
    }
}
