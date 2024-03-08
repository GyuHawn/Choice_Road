using UnityEngine;

public class Crystal : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject player;

    public GameObject crystalObj; // 크리스탈
    public GameObject crystalPoint; // 크리스탈 활성화 포인트
    public Vector3 startPos; // 크리스탈의 시작위치
    public Vector3 endPos; // 크리스탈의 목표위치
    public float moveSpd; // 이동속도
    public float rotateSpd; // 회전속도
    public bool onCrystal; // 크리스탈 활성화 여부
    private bool movingToEndPos;// 이동 방향을 나타내는 변수

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        onCrystal = false; // 크리스탈 비활성화 시작
        movingToEndPos = true;
    }

    void Update()
    {
        if (onCrystal)
        {
            crystalObj.SetActive(true); // 크리스탈 활성화
            crystalPoint.SetActive(false); // 크리스탈 활성화

            moveCrystal(); // 크리스탈 이동 함수호출
        }
        else
        {
            crystalObj.SetActive(false); // 크리스탈 비활성화
            crystalPoint.SetActive(true); // 크리스탈 비활성화
        }

        if (player.GetComponent<Collider>().bounds.Intersects(crystalPoint.GetComponent<Collider>().bounds) && playerController.getCrystal && !onCrystal)
        {
            onCrystal = true;
        }
    }

    public void moveCrystal()
    {
        // 이동 방향 설정
        Vector3 targetPos = movingToEndPos ? endPos : startPos;

        // 크리스탈 이동
        crystalObj.transform.position = Vector3.MoveTowards(crystalObj.transform.position, targetPos, moveSpd * Time.deltaTime);

        // 크리스탈 회전
        crystalObj.transform.Rotate(Vector3.forward, rotateSpd * Time.deltaTime);

        // 목표 위치 도달시 방향 변경
        if (Vector3.Distance(crystalObj.transform.position, targetPos) < 0.01f)
        {
            movingToEndPos = !movingToEndPos;
        }
    }
}
