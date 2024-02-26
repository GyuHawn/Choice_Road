using UnityEngine;

public class Crystal : MonoBehaviour
{
    public GameObject crystalObj; // ũ����Ż
    public GameObject crystalPoint; // ũ����Ż Ȱ��ȭ ����Ʈ
    public Vector3 startPos; // ũ����Ż�� ������ġ
    public Vector3 endPos; // ũ����Ż�� ��ǥ��ġ
    public float moveSpd; // �̵��ӵ�
    public float rotateSpd; // ȸ���ӵ�
    public bool onCrystal; // ũ����Ż Ȱ��ȭ ����
    private bool movingToEndPos;// �̵� ������ ��Ÿ���� ����

    void Start()
    {
        onCrystal = false; // ũ����Ż ��Ȱ��ȭ ����
        movingToEndPos = true;
    }

    void Update()
    {
        if (onCrystal)
        {
            crystalObj.SetActive(true); // ũ����Ż Ȱ��ȭ
            crystalPoint.SetActive(true); // ũ����Ż Ȱ��ȭ

            moveCrystal(); // ũ����Ż �̵� �Լ�ȣ��
        }
        else
        {
            crystalObj.SetActive(false); // ũ����Ż ��Ȱ��ȭ
            crystalPoint.SetActive(false); // ũ����Ż ��Ȱ��ȭ
        }
    }

    public void moveCrystal()
    {
        // �̵� ���� ����
        Vector3 targetPos = movingToEndPos ? endPos : startPos;

        // ũ����Ż �̵�
        crystalObj.transform.position = Vector3.MoveTowards(crystalObj.transform.position, targetPos, moveSpd * Time.deltaTime);

        // ũ����Ż ȸ��
        crystalObj.transform.Rotate(Vector3.forward, rotateSpd * Time.deltaTime);

        // ��ǥ ��ġ ���޽� ���� ����
        if (Vector3.Distance(crystalObj.transform.position, targetPos) < 0.01f)
        {
            movingToEndPos = !movingToEndPos;
        }
    }
}
