using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController cameraController;

    // �̵� ���� ����
    public float moveSpd;
    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;

    // ���� ���� ����
    public float jumpForce;
    bool jDown;
    private bool isJump;

    private Rigidbody rigid;
    private Animator anim;

    void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void Start()
    {
        // �̵��ӵ�, ���� �� ����
        moveSpd = 3f;
        jumpForce = 4f;

        // ���� ����
        isJump = false;
    }

    void Update()
    {
        // �Է� ���� �Լ� ȣ��
        GetInput();
        Move();
        Rotate();
        Jump();
        CameraMove();
    }

    private void GetInput()
    {
        // �̵����� Ű �Է� �ޱ�
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");

        // �̵��� ���
        moveVec = new Vector3(hAxis, 0, vAxis);
    }

    private void Move()
    {
        // �̵�
        transform.position += moveVec * moveSpd * Time.deltaTime;
        // �̵� �ִϸ��̼�
        anim.SetBool("Run", moveVec != Vector3.zero);
    }

    private void Rotate()
    {
        // ȸ��
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        // ���� �Է�, ���� ���� ������
        if (jDown && !isJump)
        {
            // ����
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // ���� �ִϸ��̼�
            anim.SetBool("Jump", true);
            // ���� �� ���� ����
            isJump = true;
        }
    }

    void CameraMove()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ٴ� �浹��
        if (collision.gameObject.CompareTag("Floor"))
        {
            // ���� �ִϸ��̼� ����, ���� �� ���� ����
            anim.SetBool("Jump", false);
            isJump = false;
        }
    }
}
