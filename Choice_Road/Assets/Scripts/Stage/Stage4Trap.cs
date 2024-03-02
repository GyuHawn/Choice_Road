using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Trap : MonoBehaviour
{
    private StageManager stageManager;

    public Vector3 startPos;
    public Vector3 endPos;
    public float moveSpd;

    private bool changeMove = true; // 초기에 시작 위치에서 끝 위치로 이동 시작

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Update()
    {
        if(stageManager.stageNum == 4)
        {
            // 시작 위치에서 목표 위치로 이동
            if (changeMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpd * Time.deltaTime);

                // 목표 위치 도달시 이동 방향 변경
                if (Vector3.Distance(transform.position, endPos) < 0.01f)
                {
                    changeMove = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpd * Time.deltaTime);

                // 시작 위치 도달시 이동 방향 변경
                if (Vector3.Distance(transform.position, startPos) < 0.01f)
                {
                    changeMove = true;
                }
            }
        }      
    }
}
