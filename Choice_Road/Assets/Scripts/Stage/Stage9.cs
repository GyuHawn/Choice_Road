using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage9 : MonoBehaviour
{
    private StageManager stageManager;

    public Material[] materials;
    public GameObject trapPrefab; // 함정 프리팹을 사용할 것으로 가정
    public float trapSpd;
    public int trapNum;

    public GameObject[] leftTrapPoints;
    public GameObject[] rightTrapPoints;

    public bool stage9Start;

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        stage9Start = false;
        trapNum = 6;
    }

    void Update()
    {
        if (stageManager.stageNum == 9 && !stage9Start)
        {
            stage9Start = true;
            InvokeRepeating("StartStage9", 0f, 3f);
        }
        else if (stageManager.stageNum != 9) // 아니라면 중지
        {
            stage9Start = false;
            CancelInvoke("StartStage9");
        }
    }

    void StartStage9()
    {
        // leftTrapPoints에서 랜덤으로 5개의 함정 생성
        GenerateTraps(leftTrapPoints, true);

        // rightTrapPoints에서 랜덤으로 5개의 함정 생성
        GenerateTraps(rightTrapPoints, false);
    }

    void GenerateTraps(GameObject[] trapPoints, bool Movelocation)
    {
        // 랜덤하게 선택된 함정 지점에서 함정을 생성
        for (int i = 0; i < trapNum; i++)
        {
            GameObject selectedTrapPoint = trapPoints[Random.Range(0, trapPoints.Length)];
            GameObject newTrap = Instantiate(trapPrefab, selectedTrapPoint.transform.position, Quaternion.identity);

            // 이동 방향 설정
            float moveDirection = Movelocation ? 1f : -1f;
            newTrap.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, moveDirection * trapSpd);

            // 랜덤한 Material 적용
            Renderer trapRenderer = newTrap.GetComponent<Renderer>();
            if (trapRenderer != null && materials.Length > 0)
            {
                Material randomMaterial = materials[Random.Range(0, materials.Length)];
                trapRenderer.material = randomMaterial;
            }

            Destroy(newTrap, 2f);
        }
    }
}
