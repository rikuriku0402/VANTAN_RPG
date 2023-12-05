using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int HP => _hp;
    
    public int MaxHP => _maxHp;
    
    public int Attack => _attack;
    
    [SerializeField]
    [Header("HPスライダー")]
    private Slider _hpSlider;
    
    [SerializeField]
    [Header("攻撃力")]
    private int _attack;
    
    [SerializeField]
    [Header("マックスHP")]
    private int _maxHp = 100;
    
    [SerializeField]
    [Header("HP")]
    private int _hp;
    
    private Level _level;
    
    private enum Level
    {
        LEVEL1,
        LEVEL2,
        LEVEL3,
        LEVEL4
    }
    
    void Start()
    {
        _hp = _maxHp;
        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _hp;
    }

    public void OnDamage(int damage)
    {
        RandomAttack();
        _hp -= damage;
        Debug.Log(_hp);
        if (_hp <= 0)
        {
            _hp = 0;
            Debug.Log("ゲームオーバー");
        }
        
        _hpSlider.value = _hp;
    }

    private void RandomAttack()
    {
        switch (_level)
        {
            case Level.LEVEL1:
                _attack = Random.Range(5,10);
                break;
            case Level.LEVEL2:
                _attack = Random.Range(10,20);
                break;
            case Level.LEVEL3:
                _attack = Random.Range(20,30);
                break;
            case Level.LEVEL4:
                _attack = Random.Range(40,50);
                break;
        }
    }
}
