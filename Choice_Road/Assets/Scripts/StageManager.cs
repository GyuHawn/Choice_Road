using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stageNum;
    public GameObject[] StageChangePoints;

    public GameObject player;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

       // stageNum = 0;
       stageNum = 7;
    }

    void Update()
    {
        for (int i = 0; i < StageChangePoints.Length; i++)
        {
            // 플레이어와 포인트 충돌 감지
            if (player != null && StageChangePoints[i] != null && player.GetComponent<Collider>().bounds.Intersects(StageChangePoints[i].GetComponent<Collider>().bounds))
            {
                ChangeStage(i + 1);
                break;
            }
        }
    }

    void ChangeStage(int newStageNum)
    {
        stageNum = newStageNum;
    }
}
