using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;  // 플레이어
    private float distance; // 카메라와 플레이어 거리
    public float rotateSpd; // 마우스 회전 속도
    public float minYAngle; // 최소 각도
    public float maxYAngle; // 최대 각도

    private void Start()
    {
        player = GameObject.Find("Player");

        distance = 10f;
        rotateSpd = 2f;
        minYAngle = 3f;
        maxYAngle = 80f;
    }

    private void LateUpdate()
    {
        MouseRotate();
        FollowPlayer();
    }

    private void MouseRotate() // 마우스 입력에 따라 카메라 회전
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpd;  // 수평 회전 값 계산
     
        transform.RotateAround(player.transform.position, Vector3.up, mouseX); // 플레이어 주위 회전

        float mouseY = Input.GetAxis("Mouse Y") * rotateSpd;  // 수직 회전 값 계산
     
        transform.RotateAround(player.transform.position, transform.right, -mouseY); // 플레이어 주위 수직 회전, 뒤집힘 방지하기 각도 제한

        float currentYAngle = transform.eulerAngles.x;

        if (currentYAngle < minYAngle || currentYAngle > maxYAngle)
        {
            transform.RotateAround(player.transform.position, transform.right, mouseY); // 수직 회전 각도가 범위를 벗어날시 다시 각도 제한
        }

        // 플레이어와 일정 거리를 유지, 카메라 위치 조정
        Vector3 distancePlayer = (transform.position - player.transform.position).normalized;
        transform.position = player.transform.position + distancePlayer * distance;
    }

    private void FollowPlayer() // 플레이어를 따라다니도록 설정
    {
        transform.LookAt(player.transform.position);
    }
}
