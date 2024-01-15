using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public CharacterStatus CharacterStatuList => _characterStatuList;
    
    [SerializeField]
    [Header("HPスライダー")]
    private Slider _hpSlider;
    
    [SerializeField]
    [Header("キャラクターのステータス")]
    private CharacterStatus _characterStatuList;
    
    [SerializeField]
    [Header("GSSReader")]
    private GSSReader _gssReader;
    
    [SerializeField]
    private CharaName _charaName;
    
    [SerializeField]
    private int _nameLine;
    
    [SerializeField]
    private int _hpLine;
    
    [SerializeField]
    private int _attackLine;
    
    [SerializeField]
    private int _defenseLine;
    
    [SerializeField]
    private int _magicAttackLine;
    
    [SerializeField]
    private int _magicDefenseLine;
    
    [SerializeField]
    private int _speedLine;
    
    [SerializeField]
    private int _maxHpLine;
    
    async void Start()
    {
        await WaitRoad();

        _characterStatuList.hp = _characterStatuList.maxHp;
        _hpSlider.maxValue = _characterStatuList.maxHp;
        _hpSlider.value = _characterStatuList.hp;
    }

    private async UniTask WaitRoad()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        string name = _gssReader.Datas[1][_nameLine];
        
        Debug.Log($"名前{_characterStatuList.name = _gssReader.Datas[1][_nameLine]}");
        Debug.Log($"HP{_characterStatuList.hp = int.Parse(_gssReader.Datas[1][_hpLine])}");
        Debug.Log($"物理攻撃力{_characterStatuList.attack = int.Parse(_gssReader.Datas[1][_attackLine])}");
        Debug.Log($"物理防御力{_characterStatuList.defense = int.Parse(_gssReader.Datas[1][_defenseLine])}");
        Debug.Log($"魔法攻撃力{_characterStatuList.magicAttack = int.Parse(_gssReader.Datas[1][_magicAttackLine])}");
        Debug.Log($"魔法防御力{_characterStatuList.magicDefense = int.Parse(_gssReader.Datas[1][_magicDefenseLine])}");
        Debug.Log($"素早さ{_characterStatuList.speed = int.Parse(_gssReader.Datas[1][_speedLine])}");
        // Debug.Log(_characterStatuList.maxHp = int.Parse(_gssReader.Datas[1][_maxHpLine]));
    }

    public void OnDamage(int damage)
    {
        // RandomAction();
        _characterStatuList.hp -= damage;
        Debug.Log(damage);
        if (_characterStatuList.hp <= 0)
        {
            _characterStatuList.hp = 0;
            Debug.Log("バトル終了");
        }
        // Debug.Log(_characterStatuList.type = int.Parse(_gssReader.Datas[0][_nameLine]));
        
        _hpSlider.value = _characterStatuList.hp;
    }
}

[System.Serializable]
public class CharacterStatus
{
    public string name;
    
    public int hp;
    
    public int maxHp;

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
