using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject player;
    public GameObject puzzleStart;
    public bool inGame;
    public GameObject[] puzzleObj;
    public int puzzleNum;
    public int currentPuzzle;
    public bool puzzleClear;
    public GameObject door;

    public bool isResetting = false;

    void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        inGame = false;
        puzzleNum = 10;
        currentPuzzle = 1;
        isResetting = false;
    }

    void Update()
    {

        if (!inGame && player != null && puzzleStart != null && player.GetComponent<Collider>().bounds.Intersects(puzzleStart.GetComponent<Collider>().bounds))
        {
            inGame = true;
            puzzleStart.SetActive(false);
        }

        if (inGame && !isResetting)
        {
            SelectRandomPuzzle(0);
            puzzleStart.SetActive(false);
            inGame = false;
            isResetting = true;
            StartCoroutine(ResetSetting());
        }

        if (currentPuzzle <= 0)
        {
            PuzzleClear();
        }
        else
        {
            door.SetActive(true);
        }
    }

    // 1. puzzleNum(10��)��ŭ �������� �����ϴ� �޼���
    void SelectRandomPuzzle(int num)
    {
        currentPuzzle = 1;
        List<int> selectedIndices = new List<int>();

        for (int i = 0; i < puzzleNum; i++)
        {
            int randomIndex;

            // �ߺ��� �ε����� ���ϵ��� ����
            do
            {
                randomIndex = Random.Range(0, puzzleObj.Length);
                Debug.Log(randomIndex);
            } while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);

            GameObject selectedPuzzle = puzzleObj[randomIndex];

            // 2. ���õ� puzzleObj�� Stage5Chack���� ���� ��������� �����ϰ� ������ puzzleObj�� puzzleChack�� ������ ����
            Stage5Chack chackScript = selectedPuzzle.GetComponent<Stage5Chack>();
            if (chackScript != null)
            {
                chackScript.SelectPuzzle();
            }

            num++;
        }

        currentPuzzle = num;
    }


    // 3. door�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �޼���
    void PuzzleClear()
    {
        if (door != null)
        {
            door.SetActive(false);

            foreach (var puzzle in puzzleObj)
            {
                Stage5Chack chackScript = puzzle.GetComponent<Stage5Chack>();
                if (chackScript != null)
                {
                    chackScript.puzzleChack = false;
                }
            }
        }
    }

    public void Reset()
    {
        StartCoroutine(ResetSetting());
    }

    IEnumerator ResetSetting()
    {
        Debug.Log("a");
        inGame = false;
        isResetting = false;
        yield return new WaitForSeconds(10f);

        puzzleStart.SetActive(true);
        door.SetActive(true);
        currentPuzzle = 1;
        foreach (var puzzle in puzzleObj)
        {
            Stage5Chack chackScript = puzzle.GetComponent<Stage5Chack>();
            if (chackScript != null)
            {
                chackScript.TruePuzzle();
                chackScript.puzzleChack = true;
            }
        }
    }
}
