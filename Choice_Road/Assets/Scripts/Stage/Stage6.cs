using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject[] createPos; // �� ���� ��ġ
    public GameObject trapBall; // �� ������

    public bool stage6Start; // ���� ����

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        stage6Start = false; // ���ۻ��� �ʱ�ȭ
    }

    private void Update()
    {
        // ���������� 6�̰� ���۵����ʾ����� ����
        if (stageManager.stageNum == 6 && !stage6Start)
        {
            stage6Start = true;
            InvokeRepeating("SpawnTrapBall", 0f, 5f);
        }
        else if (stageManager.stageNum != 6) // �ƴ϶�� ����
        {
            stage6Start = false;
            CancelInvoke("SpawnTrapBall");
        }
    }
  
    void SpawnTrapBall() // ���� ���� ����
    {
        // �� ���� ��ġ �� ������ ��ġ ����
        int randomIndex = Random.Range(0, createPos.Length);
        Vector3 spawnPosition = createPos[randomIndex].transform.position;

        // ������ ��ġ�� �� ����
        Instantiate(trapBall, spawnPosition, Quaternion.identity);
    }
}
