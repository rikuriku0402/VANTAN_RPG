using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    [Header("プレイヤーの情報")]
    private  Unit _playerUnit;
    
    [SerializeField]
    [Header("敵の情報")]
    private Unit _enemyUnit;
    
    [SerializeField]
    [Header("攻撃ボタン")]
    private Button _attackButton;
    
    [SerializeField]
    [Header("リザルトボタン")]
    private GameObject _resultPanel;
    
    private bool _isPlayerTurn = true;
    
    private bool _isGameOver = false;
    
    private float _second = 0f;
    
    void Start()
    {
        _isPlayerTurn = true;
        _isGameOver = false;
        _resultPanel.SetActive(false);
        _attackButton.onClick.AddListener(PushAttackButton);
    }

    private void Update()
    {
        if (_isGameOver)
        {
            ViewResult();
            return;
        }
        if (!_isPlayerTurn)
        {
            _second += Time.deltaTime;
            if (_second >= 1f)
            {
                _second = 0f;
                _isPlayerTurn = true;
                _playerUnit.OnDamage(_enemyUnit.Attack);
            }
        }

        if (_playerUnit.HP == 0 || _enemyUnit.HP == 0)
        {
            _isGameOver = true;
        }
    }

    private void PushAttackButton()
    {
        if (_isPlayerTurn)
        {
            _enemyUnit.OnDamage(_playerUnit.Attack);
            _isPlayerTurn = false;
        }
    }

    private void ViewResult()
    {
        _resultPanel.SetActive(true);
    }
}
