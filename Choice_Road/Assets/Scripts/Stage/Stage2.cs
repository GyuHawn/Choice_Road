using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    private StageManager stageManager;
 
    public GameObject[] tileGrounds; // �÷��̾ ���� Ÿ��
    public GameObject[] tiles; // Ÿ���� ������ ��ġ
    public GameObject tilePrefab; // Ÿ�� ������

    public bool stage2Start;

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        stage2Start = false;
    }

    private void Update()
    {
        if (stageManager.stageNum == 2 && !stage2Start)
        {
            stage2Start = true;
            InvokeRepeating("SelectTile", 1f, 2f);
        }
        else if (stageManager.stageNum != 2)
        {
            stage2Start = false;
            CancelInvoke("SelectTile");
        }
    }

    void SelectTile()
    {
        // �������� 5�� ��ġ ����
        List<int> selectPoints = new List<int>();
        while (selectPoints.Count < 15)
        {
            int randomIndex = Random.Range(0, tiles.Length);
            if (!selectPoints.Contains(randomIndex))
            {
                selectPoints.Add(randomIndex);
            }
        }

        foreach (int index in selectPoints)
        {
            StartCoroutine(TileSpawnChangeColor(index));
        }
    }

    IEnumerator TileSpawnChangeColor(int index) // ���õ� ��ġ�� tileGrounds ������Ʈ ���� �����ϰ�, 1�� �Ŀ� Ÿ�� ����
    {
        // �� ����
        tileGrounds[index].GetComponent<Renderer>().material.color = Color.red;

        // 1�� ��� �� Ÿ�� ����
        yield return new WaitForSeconds(1f);
        GameObject newTile = Instantiate(tilePrefab, tiles[index].transform.position, Quaternion.identity);

        // 0.5�� ��� �� �� ����
        yield return new WaitForSeconds(0.5f);
        tileGrounds[index].GetComponent<Renderer>().material.color = Color.white;

        // 3�� �Ŀ� ������ Ÿ�� ����
        Destroy(newTile, 2f);
    }
}
