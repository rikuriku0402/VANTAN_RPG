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

    [Header("�ړ����x")]
    public float moveSpeed;

    private Rigidbody2D rb;                      // �R���|�[�l���g�̎擾�p

    private float horizontal;                    // x ��(�����E��)�����̓��͂̒l�̑���p
    private float vertical;                      // y ��(�����E�c)�����̓��͂̒l�̑���p

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
        // ���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���R���|�[�l���g�̒�����A<�w��>�����R���|�[�l���g�̏����擾���āA���ӂɗp�ӂ����ϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();     // ���邢�́ATryGeyComponent(out rb);�@�ł���
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

        // InputManager �� Horizontal �ɓo�^���Ă���L�[�����͂��ꂽ��A����(��)�����̓��͒l�Ƃ��đ��
        horizontal = Input.GetAxis("Horizontal");

        // InputManager �� Vertical �ɓo�^���Ă���L�[�����͂��ꂽ��A����(��)�����̓��͒l�Ƃ��đ��
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

        // �ړ�
        Move();
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {

        // �΂߈ړ��̋����������Ȃ��悤�ɐ��K���������s���A�P�ʃx�N�g���Ƃ���(�����̏��͎����A�����ɂ�鑬�x�����Ȃ����Ĉ��l�ɂ���)
        Vector3 dir = new Vector3(horizontal, vertical, 0).normalized;

        // velocity(���x)�ɐV�����l�������āA�Q�[���I�u�W�F�N�g���ړ�������
        rb.velocity = dir * moveSpeed;
    }
    private async UniTask Wait(int time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        _enemySpown._konaideRock = false;

    }
}