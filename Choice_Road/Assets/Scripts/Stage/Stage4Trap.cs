using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Trap : MonoBehaviour
{
    private StageManager stageManager;

    public Vector3 startPos;
    public Vector3 endPos;
    public float moveSpd;

    private bool changeMove = true; // �ʱ⿡ ���� ��ġ���� �� ��ġ�� �̵� ����

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Update()
    {
        if(stageManager.stageNum == 4)
        {
            // ���� ��ġ���� ��ǥ ��ġ�� �̵�
            if (changeMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpd * Time.deltaTime);

                // ��ǥ ��ġ ���޽� �̵� ���� ����
                if (Vector3.Distance(transform.position, endPos) < 0.01f)
                {
                    changeMove = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpd * Time.deltaTime);

                // ���� ��ġ ���޽� �̵� ���� ����
                if (Vector3.Distance(transform.position, startPos) < 0.01f)
                {
                    changeMove = true;
                }
            }
        }      
    }
}
