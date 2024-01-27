using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("�ړ����x")]
    public float moveSpeed;

    private Rigidbody2D rb;                      // �R���|�[�l���g�̎擾�p

    private float horizontal;                    // x ��(�����E��)�����̓��͂̒l�̑���p
    private float vertical;                      // y ��(�����E�c)�����̓��͂̒l�̑���p

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
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // ���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���R���|�[�l���g�̒�����A<�w��>�����R���|�[�l���g�̏����擾���āA���ӂɗp�ӂ����ϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();     // ���邢�́ATryGeyComponent(out rb);�@�ł���
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
            _spriteRenderer.sprite = _backRiku;
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            // this.transform.Translate(0.0f, 0.0f, -0.1f);
            _spriteRenderer.sprite = _kurunaRiku;
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.Translate(-0.1f, 0.0f, 0.0f);
            _spriteRenderer.sprite = _leftRiku;
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            // this.transform.Translate(0.1f, 0.0f, 0.0f);
            _spriteRenderer.sprite = _rightRiku;
        }
        else
            _spriteRenderer.sprite = _frontRiku;

        
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
}