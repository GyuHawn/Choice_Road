using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;  // �÷��̾�
    private float distance = 10f;  // ī�޶�� �÷��̾� �Ÿ�
    public float rotateSpd = 2f;  // ���콺 ȸ�� �ӵ�
    public float minYAngle = 10f;  // �ּ� ����
    public float maxYAngle = 80f;  //  �ִ� ����

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void LateUpdate()
    {
        MouseRotate();
        FollowPlayer();
    }

    private void MouseRotate() // ���콺 �Է¿� ���� ī�޶� ȸ��
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpd;  // ���� ȸ�� �� ���
     
        transform.RotateAround(player.transform.position, Vector3.up, mouseX); // �÷��̾� ���� ȸ��

        float mouseY = Input.GetAxis("Mouse Y") * rotateSpd;  // ���� ȸ�� �� ���
     
        transform.RotateAround(player.transform.position, transform.right, -mouseY); // �÷��̾� ���� ���� ȸ��, ������ �����ϱ� ���� ����

        float currentYAngle = transform.eulerAngles.x;

        if (currentYAngle < minYAngle || currentYAngle > maxYAngle)
        {
            transform.RotateAround(player.transform.position, transform.right, mouseY); // ���� ȸ�� ������ ������ ����� �ٽ� ���� ����
        }

        // �÷��̾�� ���� �Ÿ��� ����, ī�޶� ��ġ ����
        Vector3 distancePlayer = (transform.position - player.transform.position).normalized;
        transform.position = player.transform.position + distancePlayer * distance;
    }

    private void FollowPlayer() // �÷��̾ ����ٴϵ��� ����
    {
        transform.LookAt(player.transform.position);
    }
}
