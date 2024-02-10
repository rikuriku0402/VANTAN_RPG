using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCommand : MonoBehaviour
{
    [SerializeField]
    [Header("アクションボタン")]
    private Button _actionButton;
    
    [SerializeField]
    [Header("キャラクター変更ボタン")]
    private Button _charaChangeButton;
    
    [SerializeField]
    [Header("戻るボタン")]
    private Button _backButton;
    
    [SerializeField]
    [Header("アクションボタンオブジェクト")]
    private GameObject _actionButtonObj;
    
    [SerializeField]
    [Header("キャラクター変更ボタンオブジェクト")]
    private GameObject _charaChangeButtonObj;
    
    void Start()
    {
        _actionButton.onClick.AddListener(ActionButton);
        _charaChangeButton.onClick.AddListener(CharaChangeButton);
        
        _actionButtonObj.SetActive(false);
        _charaChangeButtonObj.SetActive(false);
    }

    private void ActionButton()
    {
        _actionButtonObj.SetActive(true);
        _charaChangeButtonObj.SetActive(false);
        
        _actionButton.gameObject.SetActive(false);
        _charaChangeButton.gameObject.SetActive(false);
    }

    private void CharaChangeButton()
    {
        _actionButtonObj.SetActive(false);
        _charaChangeButtonObj.SetActive(true);
        
        _actionButton.gameObject.SetActive(false);
        _charaChangeButton.gameObject.SetActive(false);
    }

    public void BackButton()
    {
        _actionButton.gameObject.SetActive(true);
        _charaChangeButton.gameObject.SetActive(true);
        
        _actionButtonObj.SetActive(false);
        _charaChangeButtonObj.SetActive(false);
    }
}
