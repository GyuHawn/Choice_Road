using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;  // �÷��̾�
    private float distance; // ī�޶�� �÷��̾� �Ÿ�
    public float rotateSpd; // ���콺 ȸ�� �ӵ�
    public float minYAngle; // �ּ� ����
    public float maxYAngle; // �ִ� ����

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
