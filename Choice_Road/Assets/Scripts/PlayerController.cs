using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController cameraController;
    public float moveSpd;
    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;
    private float rotateSpd = 5f;

    public float jumpForce;
    bool jDown;
    private bool isJump;

    private Rigidbody rigid;
    private Animator anim;

    void Awake()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        rigid = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        moveSpd = 3f;
        jumpForce = 4f;
        isJump = false;
    }

    void Update()
    {
        GetInput();
        Move();
        Rotate();
        Jump();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");

        Vector3 cameraForward = cameraController.transform.forward;
        cameraForward.y = 0f;
        moveVec = (vAxis * cameraForward + hAxis * cameraController.transform.right).normalized;
    }

    private void Move()
    {
        transform.position += moveVec * moveSpd * Time.deltaTime;
        anim.SetBool("Run", moveVec != Vector3.zero);
    }

    private void Rotate()
    {
        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpd);
        }
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("Jump", true);
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("Jump", false);
            isJump = false;
        }
    }
}
