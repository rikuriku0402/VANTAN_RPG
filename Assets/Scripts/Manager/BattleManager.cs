using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    [Header("キャラの情報")]
    private  Unit _playerUnit;
    
    [SerializeField]
    [Header("敵の情報")]
    private Unit _enemyUnit;
    
    [SerializeField]
    [Header("GameManager")]
    private GameManager _gameManager;
    
    [SerializeField]
    [Header("攻撃ボタン")]
    private Button _attackButton;
    
    [SerializeField]
    [Header("魔法攻撃ボタン")]
    private Button _magicAttackButton;
    
    private bool _isPlayerTurn = true;
    
    private float _second = 0f;
    
    private int i = 0;
    
    private CharaName _charaName;
    
    void Start()
    {
        _isPlayerTurn = true;
        
        // ボタンへの登録
        _attackButton.onClick.AddListener(PushAttackButton);
        _magicAttackButton.onClick.AddListener(PushMagicAttackButton);
    }

    private void Update()
    {
        EnemyTurn();

        // ゲームオーバー判定
        if (_playerUnit.CharacterStatuList.hp <= 0 && _gameManager.IsGame)
        {
            _gameManager.GameOver();
            _gameManager.GameModeChange(false);
            return;
        }
        
        if (_enemyUnit.CharacterStatuList.hp <= 0 && !_gameManager.IsGame)
        {
            _gameManager.GameClear();
            _gameManager.GameModeChange(true);
            return;
        }
    }

    private void PushAttackButton()
    {
        if (_isPlayerTurn)
        {
            _enemyUnit.OnDamage(_playerUnit.CharacterStatuList.attack);
            _isPlayerTurn = false;
        }
    }

    private void PushMagicAttackButton()
    {
        if (_isPlayerTurn)
        {
            _enemyUnit.OnDamage(_playerUnit.CharacterStatuList.magicAttack);
            _isPlayerTurn = false;
        }
    }

    private void EnemyTurn()
    {
        i = Random.Range(0,1);
        
        if (!_isPlayerTurn)
        {
            _second += Time.deltaTime;
            if (i == 0)
            {
                if (_second >= 1f)
                {
                    _second = 0f;
                    _isPlayerTurn = true;
                    _playerUnit.OnDamage(_enemyUnit.CharacterStatuList.attack);
                    
                    Debug.Log("通常攻撃");
                }
            }
            else
            {
                if (_second >= 1f)
                {
                    _second = 0f;
                    _isPlayerTurn = true;
                    _playerUnit.OnDamage(_enemyUnit.CharacterStatuList.magicAttack);
                    
                    Debug.Log("魔法攻撃");
                }
            }
        }
    }
}
