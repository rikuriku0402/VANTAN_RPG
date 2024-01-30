using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour
{
    public CharacterStatus CharacterStatusList => _characterStatusList;
    
    [SerializeField]
    [Header("HPスライダー")]
    private Slider _hpSlider;
    
    [SerializeField]
    [Header("GSSReader")]
    private GSSReader _gssReader;
    
    [SerializeField]
    private CharacterStatus _characterStatusList;
    
    [SerializeField]
    [Header("バトルマネージャー")]
    private BattleManager _battleManager;
    
    [SerializeField]
    [Header("HPテキスト")]
    private Text _hpText;

    [SerializeField]
    private int _charaNum;
    
    [SerializeField]
    private bool _isEnemy;
    
    [SerializeField]
    [Header("キャラクタースプライト")]
    private CharacterSprite _characterSprite;
    
    private const int NAMELINE = 0;
    
    private const int HPLINE = 1;
    
    private const int ATTACKLINE = 2;
    
    private const int DEFENSELINE = 3;
    
    private const int MAGICATTACKLINE = 4;
    
    private const int MAGICDEFENSELINE = 5;
    
    private const int SPEEDLINE = 6; 
    
    private int _maxHp;
    
    private CancellationToken token;
    
    
    private async void Start()
    {
        Debug.Log("ロード開始");
        
        await WaitRoad();
        _characterStatusList.hp = _characterStatusList.hp;
        _hpSlider.maxValue = _characterStatusList.hp;
        _hpSlider.value = _characterStatusList.hp;
        
        _maxHp = _characterStatusList.hp;
        
        _hpText.text = _characterStatusList.hp + "/" + _maxHp;
    }

    private async UniTask WaitRoad()
    { 
        token = this.GetCancellationTokenOnDestroy();
        await UniTask.WaitUntil(() => !_gssReader.IsLoading, cancellationToken: token);
        
        _characterStatusList.name = _gssReader.Datas[_charaNum][NAMELINE];
        _characterStatusList.hp = int.Parse(_gssReader.Datas[_charaNum][HPLINE]);
        _characterStatusList.attack = int.Parse(_gssReader.Datas[_charaNum][ATTACKLINE]);
        _characterStatusList.defense = int.Parse(_gssReader.Datas[_charaNum][DEFENSELINE]);
        _characterStatusList.magicAttack = int.Parse(_gssReader.Datas[_charaNum][MAGICATTACKLINE]);
        _characterStatusList.magicDefense = int.Parse(_gssReader.Datas[_charaNum][MAGICDEFENSELINE]);
        _characterStatusList.speed = int.Parse(_gssReader.Datas[_charaNum][SPEEDLINE]);

        if (_isEnemy)
        {
            int random = Random.Range(2, 4);
            
            _characterStatusList.name = _gssReader.Datas[random][NAMELINE];
            _characterStatusList.hp = int.Parse(_gssReader.Datas[random][HPLINE]);
            _characterStatusList.attack = int.Parse(_gssReader.Datas[random][ATTACKLINE]);
            _characterStatusList.defense = int.Parse(_gssReader.Datas[random][DEFENSELINE]);
            _characterStatusList.magicAttack = int.Parse(_gssReader.Datas[random][MAGICATTACKLINE]);
            _characterStatusList.magicDefense = int.Parse(_gssReader.Datas[random][MAGICDEFENSELINE]);
            _characterStatusList.speed = int.Parse(_gssReader.Datas[random][SPEEDLINE]);
            
            _characterSprite.EnemyImageRoad(random);
        }

        Debug.Log("名前" + _characterStatusList.name);
        Debug.Log("HP" + _characterStatusList.hp);
        Debug.Log("物理攻撃力" + _characterStatusList.attack);
        Debug.Log("物理防御力" + _characterStatusList.defense);
        Debug.Log("魔法攻撃力" + _characterStatusList.magicAttack);
        Debug.Log("魔法防御力" + _characterStatusList.magicDefense);
        Debug.Log("素早さ" + _characterStatusList.speed);
        
        _battleManager.gameObject.SetActive(true);
    }

    public void OnDamage(int damage)
    {
        _characterStatusList.hp -= damage;
        Debug.Log(damage);
        
        _hpSlider.value = _characterStatusList.hp;
        _hpText.text = _characterStatusList.hp + "/" + _maxHp;

        if (_characterStatusList.hp <= 0)
        {
            _hpSlider.value = 0;
            _hpText.text = 0 + "/" + _maxHp;
            Debug.Log(_characterStatusList.name + "は死んだ");
        }
    }
}

[System.Serializable]
public class CharacterStatus
{
    public string name;
    
    public int hp;
    
    // public int maxHp;

    public int attack;
    
    public int defense;
    
    public int magicAttack;
    
    public int magicDefense;
    
    public int speed;

    public Type type;
    
    public enum Type
    {
        FIRE,
        WATER,
        TREE,
    }
}
