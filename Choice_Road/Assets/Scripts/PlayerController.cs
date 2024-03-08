using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController cameraController;
    private StageManager stageManager; 
    private Stage1 stage1;
    private Stage4 stage4;
    private Stage5 stage5;
    private Stage10 stage10;

    public float moveSpd;
    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;
    private float rotateSpd = 5f;

    public float jumpForce;
    bool jDown;
    private bool isJump;

    public bool getCrystal;

    private Rigidbody rigid;
    private Animator anim;

    public GameObject mainCamera;

    void Awake()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        stage1 = GameObject.Find("Manager").GetComponent<Stage1>();
        stage4 = GameObject.Find("Manager").GetComponent<Stage4>();
        stage5 = GameObject.Find("Manager").GetComponent<Stage5>();
        rigid = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        moveSpd = 4f;
        jumpForce = 4f;
        isJump = false;

        getCrystal = false;
    }

    void Update()
    {   
        // 이동관련
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

    void Die()
    {
        // 처음으로 이동
        gameObject.transform.position = new Vector3(0, 0, 0);
        mainCamera.transform.position = new Vector3(0, 4, -6);
        
        if(stageManager.stageNum == 5)
        {
            stage5.Reset();
        }
        else if(stageManager.stageNum == 10)
        {
            stage10.ResetSetting();
        }

        // 스테이지 초기화
        stageManager.stageNum = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 점프 관련
        if (collision.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("Jump", false);
            isJump = false;
        }
        else if (collision.gameObject.CompareTag("JumpFloor") || collision.gameObject.CompareTag("Stage5Puzzle"))
        {
            jumpForce = 6;
            anim.SetBool("Jump", false);
            isJump = false;
        }

        // 스테이지 기능 관련
        if (collision.gameObject.CompareTag("Stage1"))
        {
            stage1.SelectBridge();
        }
        else if (collision.gameObject.CompareTag("Stage4Trap"))
        {
            stage4.MazeTrap();
        }
        else if (collision.gameObject.CompareTag("Stage5Puzzle"))
        {
            Stage5Chack puzzleObj = collision.gameObject.GetComponent<Stage5Chack>();

            if (puzzleObj.puzzleChack)
            {
                Die();
            }
            else
            {
                puzzleObj.TruePuzzle();
            }
        }

        // 사망 관련
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Die")) // 트랩, 떨어짐
        {
            Die();
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpFloor"))
        {
            jumpForce = 4;
        }
    }
}
