using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController cameraController;

    // 이동 관련 변수
    public float moveSpd;
    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;

    // 점프 관련 변수
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
        // 이동속도, 점프 값 설정
        moveSpd = 3f;
        jumpForce = 4f;

        // 점프 상태
        isJump = false;
    }

    void Update()
    {
        // 입력 받을 함수 호출
        GetInput();
        Move();
        Rotate();
        Jump();
        CameraMove();
    }

    private void GetInput()
    {
        // 이동관련 키 입력 받기
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");

        // 이동값 계산
        moveVec = new Vector3(hAxis, 0, vAxis);
    }

    private void Move()
    {
        // 이동
        transform.position += moveVec * moveSpd * Time.deltaTime;
        // 이동 애니메이션
        anim.SetBool("Run", moveVec != Vector3.zero);
    }

    private void Rotate()
    {
        // 회전
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        // 점프 입력, 아직 점프 중인지
        if (jDown && !isJump)
        {
            // 점프
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // 점프 애니메이션
            anim.SetBool("Jump", true);
            // 점프 중 상태 변경
            isJump = true;
        }
    }

    void CameraMove()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥 충돌시
        if (collision.gameObject.CompareTag("Floor"))
        {
            // 점프 애니메이션 해제, 점프 중 상태 변경
            anim.SetBool("Jump", false);
            isJump = false;
        }
    }
}
