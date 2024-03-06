using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage9 : MonoBehaviour
{
    private StageManager stageManager;

    public Material[] materials;
    public GameObject trapPrefab; // ���� �������� ����� ������ ����
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
        else if (stageManager.stageNum != 9) // �ƴ϶�� ����
        {
            stage9Start = false;
            CancelInvoke("StartStage9");
        }
    }

    void StartStage9()
    {
        // leftTrapPoints���� �������� 5���� ���� ����
        GenerateTraps(leftTrapPoints, true);

        // rightTrapPoints���� �������� 5���� ���� ����
        GenerateTraps(rightTrapPoints, false);
    }

    void GenerateTraps(GameObject[] trapPoints, bool Movelocation)
    {
        // �����ϰ� ���õ� ���� �������� ������ ����
        for (int i = 0; i < trapNum; i++)
        {
            GameObject selectedTrapPoint = trapPoints[Random.Range(0, trapPoints.Length)];
            GameObject newTrap = Instantiate(trapPrefab, selectedTrapPoint.transform.position, Quaternion.identity);

            // �̵� ���� ����
            float moveDirection = Movelocation ? 1f : -1f;
            newTrap.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, moveDirection * trapSpd);

            // ������ Material ����
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
