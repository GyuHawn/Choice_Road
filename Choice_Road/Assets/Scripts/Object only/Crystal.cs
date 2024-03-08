using UnityEngine;

public class Crystal : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject player;

    public GameObject crystalObj; // ũ����Ż
    public GameObject crystalPoint; // ũ����Ż Ȱ��ȭ ����Ʈ
    public Vector3 startPos; // ũ����Ż�� ������ġ
    public Vector3 endPos; // ũ����Ż�� ��ǥ��ġ
    public float moveSpd; // �̵��ӵ�
    public float rotateSpd; // ȸ���ӵ�
    public bool onCrystal; // ũ����Ż Ȱ��ȭ ����
    private bool movingToEndPos;// �̵� ������ ��Ÿ���� ����

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
        onCrystal = false; // ũ����Ż ��Ȱ��ȭ ����
        movingToEndPos = true;
    }

    void Update()
    {
        if (onCrystal)
        {
            crystalObj.SetActive(true); // ũ����Ż Ȱ��ȭ
            crystalPoint.SetActive(false); // ũ����Ż Ȱ��ȭ

            moveCrystal(); // ũ����Ż �̵� �Լ�ȣ��
        }
        else
        {
            crystalObj.SetActive(false); // ũ����Ż ��Ȱ��ȭ
            crystalPoint.SetActive(true); // ũ����Ż ��Ȱ��ȭ
        }

        if (player.GetComponent<Collider>().bounds.Intersects(crystalPoint.GetComponent<Collider>().bounds) && playerController.getCrystal && !onCrystal)
        {
            onCrystal = true;
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
