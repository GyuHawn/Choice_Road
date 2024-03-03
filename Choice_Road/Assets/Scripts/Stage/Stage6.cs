using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject[] createPos; // 공 생성 위치
    public GameObject trapBall; // 공 프리팹

    public bool stage6Start; // 시작 여부

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        stage6Start = false; // 시작상태 초기화
    }

    private void Update()
    {
        // 스테이지가 6이고 시작되지않았을때 시작
        if (stageManager.stageNum == 6 && !stage6Start)
        {
            stage6Start = true;
            InvokeRepeating("SpawnTrapBall", 0f, 5f);
        }
        else if (stageManager.stageNum != 6) // 아니라면 중지
        {
            stage6Start = false;
            CancelInvoke("SpawnTrapBall");
        }
    }
  
    void SpawnTrapBall() // 함정 공을 생성
    {
        // 공 생성 위치 중 무작위 위치 선택
        int randomIndex = Random.Range(0, createPos.Length);
        Vector3 spawnPosition = createPos[randomIndex].transform.position;

        // 선택한 위치에 공 생성
        Instantiate(trapBall, spawnPosition, Quaternion.identity);
    }
}
