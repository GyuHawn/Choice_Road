using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    private StageManager stageManager;
 
    public GameObject[] tileGrounds; // 플레이어가 밟은 타일
    public GameObject[] tiles; // 타일을 생성할 위치
    public GameObject tilePrefab; // 타일 프리팹

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
        // 랜덤으로 5개 위치 선택
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

    IEnumerator TileSpawnChangeColor(int index) // 선택된 위치의 tileGrounds 오브젝트 색을 변경하고, 1초 후에 타일 생성
    {
        // 색 변경
        tileGrounds[index].GetComponent<Renderer>().material.color = Color.red;

        // 1초 대기 후 타일 생성
        yield return new WaitForSeconds(1f);
        GameObject newTile = Instantiate(tilePrefab, tiles[index].transform.position, Quaternion.identity);

        // 0.5초 대기 후 색 변경
        yield return new WaitForSeconds(0.5f);
        tileGrounds[index].GetComponent<Renderer>().material.color = Color.white;

        // 3초 후에 생성한 타일 삭제
        Destroy(newTile, 2f);
    }
}
