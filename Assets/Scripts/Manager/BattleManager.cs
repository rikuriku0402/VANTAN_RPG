using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    [Header("リクの情報")]
    private  Unit _rinkuUnit;
    
    [SerializeField]
    [Header("アベの情報")]
    private Unit _abeUnit;

    [SerializeField]
    [Header("オノの情報")]
    private Unit _onoUnit;

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
    [Header("防御ボタン")]
    private Button _defenseButton;
    
    [SerializeField]
    [Header("魔法攻撃ボタン")]
    private Button _magicAttackButton;
    
    [SerializeField]
    [Header("魔法防御ボタン")]
    private Button _magicDefenseButton;
    
    [SerializeField]
    [Header("バトルテキストクラス")]
    private BattleText _battleText;
    
    private bool _isPlayerTurn = true;
    
    private bool _isDefense = true;
    
    private float _second = 0f;
    
    private int _charaNum;
    
    private int _enemyAttack;

    void Start()
    {
        _isPlayerTurn = true;
        
        Debug.Log(_rinkuUnit.CharacterStatusList.hp);
        
        // ボタンへの登録
        _attackButton.onClick.AddListener(PushAttackButton);
        _magicAttackButton.onClick.AddListener(PushMagicAttackButton);
        _defenseButton.onClick.AddListener(PushDefenseButton);
        _magicDefenseButton.onClick.AddListener(PushMagicDefenseButton);

        _abeUnit.gameObject.SetActive(false);
        _onoUnit.gameObject.SetActive(false);
    }

    private void Update()
    {
        // ゲームオーバー判定
        if (_rinkuUnit.CharacterStatusList.hp <= 0 && 
            _abeUnit.CharacterStatusList.hp <= 0 && 
            _onoUnit.CharacterStatusList.hp <= 0)
        {            
            Debug.Log("ゲームオーバー");
            _gameManager.GameOver();
        }
        
        EnemyTurn();

        if (_enemyUnit.CharacterStatusList.hp <= 0)
        {
            _gameManager.GameClear();
            // return;
        }
    }

    private void PushAttackButton()
    {
        if (_isPlayerTurn)
        {
            if (_rinkuUnit.gameObject.activeSelf)
            {
                _enemyUnit.OnDamage(_rinkuUnit.CharacterStatusList.attack);
                _battleText.BattleLog(_rinkuUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _rinkuUnit.CharacterStatusList.attack);
            }
            else if (_abeUnit.gameObject.activeSelf)
            {
                _enemyUnit.OnDamage(_abeUnit.CharacterStatusList.attack);
                _battleText.BattleLog(_abeUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _abeUnit.CharacterStatusList.attack);
            }
            else if (_onoUnit.gameObject.activeSelf)
            {
                _enemyUnit.OnDamage(_onoUnit.CharacterStatusList.attack);
                _battleText.BattleLog(_onoUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _onoUnit.CharacterStatusList.attack);
            }
            
            _isPlayerTurn = false;
        }
    }


    private void PushMagicAttackButton()
    {
        if (_isPlayerTurn)
        {
            if (_rinkuUnit.gameObject.activeSelf)
            {
                _enemyUnit.OnDamage(_rinkuUnit.CharacterStatusList.magicAttack);
                _battleText.BattleLog(_rinkuUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _rinkuUnit.CharacterStatusList.attack);
            }
            else if (_abeUnit.gameObject.activeSelf)
            {
                _enemyUnit.OnDamage(_abeUnit.CharacterStatusList.magicAttack);
                _battleText.BattleLog(_abeUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _abeUnit.CharacterStatusList.attack);
            }
            else if (_onoUnit.gameObject.activeSelf)
            {
                _enemyUnit.OnDamage(_onoUnit.CharacterStatusList.magicAttack);
                _battleText.BattleLog(_onoUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _onoUnit.CharacterStatusList.attack);
            }
            
            _isPlayerTurn = false;
        }
    }

    private void PushDefenseButton()
    {
        if (_isPlayerTurn)
        {
            if (_isDefense)
            {
                if (_rinkuUnit.gameObject.activeSelf)
                {
                    PlayerDefense(_rinkuUnit.CharacterStatusList.defense);
                    _battleText.BattleLog(_rinkuUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _rinkuUnit.CharacterStatusList.attack);
                }
                else if (_abeUnit.gameObject.activeSelf)
                {
                    PlayerDefense(_abeUnit.CharacterStatusList.defense);
                    _battleText.BattleLog(_abeUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _abeUnit.CharacterStatusList.attack);
                }
                else if (_onoUnit.gameObject.activeSelf)
                {
                    PlayerDefense(_onoUnit.CharacterStatusList.defense);
                    _battleText.BattleLog(_onoUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _onoUnit.CharacterStatusList.attack);
                }
                
                _isPlayerTurn = false;
                _isDefense = false;
            }
        }
    }
    
    private void PushMagicDefenseButton()
    {
        if (_isPlayerTurn)
        {
            if (_isDefense)
            {
                if (_rinkuUnit.gameObject.activeSelf)
                {
                    PlayerDefense(_rinkuUnit.CharacterStatusList.magicDefense);
                    _battleText.BattleLog(_rinkuUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _rinkuUnit.CharacterStatusList.attack);
                }
                else if (_abeUnit.gameObject.activeSelf)
                {
                    PlayerDefense(_abeUnit.CharacterStatusList.magicDefense);
                    _battleText.BattleLog(_abeUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _abeUnit.CharacterStatusList.attack);
                }
                else if (_onoUnit.gameObject.activeSelf)
                {
                    PlayerDefense(_onoUnit.CharacterStatusList.magicDefense);
                    _battleText.BattleLog(_onoUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.name, _onoUnit.CharacterStatusList.attack);
                }
                
                _isPlayerTurn = false;
                _isDefense = false;
            }
        }
    }

    private void EnemyTurn()
    {
        // int i = Random.Range(0,1);
        
        if (!_isPlayerTurn)
        {
            _second += Time.deltaTime;

            if (_second >= 1f)
            {
                _second = 0f;
                    
                if (!_isDefense)
                {
                    _isPlayerTurn = true;
                    _isDefense = true;
                    return;
                }
                    
                if (_rinkuUnit.gameObject.activeSelf)
                {
                    _rinkuUnit.OnDamage(_enemyUnit.CharacterStatusList.attack);
                    _battleText.BattleLog(_enemyUnit.CharacterStatusList.name, _rinkuUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.attack);
                    
                    if (_rinkuUnit.CharacterStatusList.hp <= 0)
                    {
                        _rinkuUnit.gameObject.SetActive(false);
                        _abeUnit.gameObject.SetActive(true);
                    }
                }
                else if (_abeUnit.gameObject.activeSelf)
                {
                    _abeUnit.OnDamage(_enemyUnit.CharacterStatusList.attack);
                    _battleText.BattleLog(_enemyUnit.CharacterStatusList.name, _abeUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.attack);
                    
                    if (_abeUnit.CharacterStatusList.hp <= 0)
                    {
                        _abeUnit.gameObject.SetActive(false);
                        _onoUnit.gameObject.SetActive(true);
                    }
                }
                else if (_onoUnit.gameObject.activeSelf)
                {
                    _onoUnit.OnDamage(_enemyUnit.CharacterStatusList.attack);
                    _battleText.BattleLog(_enemyUnit.CharacterStatusList.name, _onoUnit.CharacterStatusList.name, _enemyUnit.CharacterStatusList.attack);
                    
                    if (_onoUnit.CharacterStatusList.hp <= 0)
                    {
                        _onoUnit.gameObject.SetActive(false);
                        _rinkuUnit.gameObject.SetActive(true);
                    }
                }
                    
                _isPlayerTurn = true;
                Debug.Log("通常攻撃");
            }
        }
    }

    private void PlayerDefense(int defense)
    {
        var enemyAttack = _enemyUnit.CharacterStatusList.attack - defense;
        
        if (enemyAttack <= 0) enemyAttack = 0;
        _rinkuUnit.OnDamage(enemyAttack);
        
        Debug.Log(enemyAttack);
    }
}
