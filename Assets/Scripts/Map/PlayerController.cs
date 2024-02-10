using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Cysharp.Threading.Tasks;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("移動速度")]
    public float moveSpeed;

    private Rigidbody2D rb;                      // コンポーネントの取得用

    private float horizontal;                    // x 軸(水平・横)方向の入力の値の代入用
    private float vertical;                      // y 軸(垂直・縦)方向の入力の値の代入用

    [SerializeField] private Animator animator;

    public static Vector3 _playerPos = new();

    [SerializeField]
    Sprite _frontRiku;
    [SerializeField]
    Sprite _backRiku;
    [SerializeField]
    Sprite _rightRiku;
    [SerializeField]
    Sprite _leftRiku;

    [SerializeField]
    Sprite _kurunaRiku;

    [SerializeField]
    EnemySpown _enemySpown;
    private SpriteRenderer _spriteRenderer;

    async void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // このスクリプトがアタッチされているゲームオブジェクトにアタッチされているコンポーネントの中から、<指定>したコンポーネントの情報を取得して、左辺に用意した変数に代入
        rb = GetComponent<Rigidbody2D>();     // あるいは、TryGeyComponent(out rb);　でも可
        _enemySpown._konaideRock = true;

        switch (MapSceneMove._maeScene)
        {
            case "Ippan":
                this.gameObject.transform.position = new Vector3(9, 2.5f, 0);
                break;
            case "Oikawa":
                this.gameObject.transform.position = new Vector3(-9, 2.5f, 0);
                break;
            case "zyosi":
                this.gameObject.transform.position = new Vector3(0.6f, 12, 0);
                break;
            default:

                break;
        }
        await Wait(5);
    }

    void Update()
    {

        // InputManager の Horizontal に登録してあるキーが入力されたら、水平(横)方向の入力値として代入
        horizontal = Input.GetAxis("Horizontal");

        // InputManager の Vertical に登録してあるキーが入力されたら、水平(横)方向の入力値として代入
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.W))
        {
            //  this.transform.Translate(0.0f, 0.0f, 0.1f);
            //_spriteRenderer.sprite = _backRiku;
            animator.SetBool("back", true);
        }
        else
            animator.SetBool("back", false);
       

        if (Input.GetKey(KeyCode.S))
        {
            // this.transform.Translate(0.0f, 0.0f, -0.1f);
            //_spriteRenderer.sprite = _kurunaRiku;
            animator.SetBool("flont", true);
        }
        else
        animator.SetBool("flont", false);
       
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.Translate(-0.1f, 0.0f, 0.0f);
            //_spriteRenderer.sprite = _leftRiku;
            animator.SetBool("reft", true);
        }
        else
        animator.SetBool("reft", false);
        

        if (Input.GetKey(KeyCode.D))
        {
            // this.transform.Translate(0.1f, 0.0f, 0.0f);
            //_spriteRenderer.sprite = _rightRiku;
            animator.SetBool("light", true);
        }
        else
        animator.SetBool("light", false);



        //animator.SetInteger("MoveID", 0);
        //_spriteRenderer.sprite = _frontRiku;


    }

    void FixedUpdate()
    {

        // 移動
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {

        // 斜め移動の距離が増えないように正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする)
        Vector3 dir = new Vector3(horizontal, vertical, 0).normalized;

        // velocity(速度)に新しい値を代入して、ゲームオブジェクトを移動させる
        rb.velocity = dir * moveSpeed;
    }
    private async UniTask Wait(int time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        _enemySpown._konaideRock = false;

    }
}