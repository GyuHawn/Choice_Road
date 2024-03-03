using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : MonoBehaviour
{
    public GameObject player; // �÷��̾�
    public GameObject puzzleStart; // ���� ���� ����
    public bool inGame; // ���� ���ΰ�
    public GameObject[] puzzleObj; // ���� ������Ʈ
    public int puzzleNum; // ���� ������ ������Ʈ ����
    public int currentPuzzle; // ���� ���°� ����� ������Ʈ
    public bool puzzleClear; // ���� Ŭ����
    public GameObject door; // ��

    public bool isResetting = false; // ���� �����Ѱ�

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        // �⺻ ���� �ʱ�ȭ
        inGame = false;
        puzzleNum = 10;
        currentPuzzle = 1;
        isResetting = false;
    }

    void Update()
    {
        // ������ x, �÷��̾� ����, �÷��̾ ���� ���������� �浹�� ���� ����
        if (!inGame && player != null && puzzleStart != null && player.GetComponent<Collider>().bounds.Intersects(puzzleStart.GetComponent<Collider>().bounds))
        {
            inGame = true;
            puzzleStart.SetActive(false);
        }

        // ������ o, ���ºҰ�
        if (inGame && !isResetting)
        {
            SelectRandomPuzzle(0); // ���� ���� ����
            puzzleStart.SetActive(false); // ���� ���� ��Ȱ��ȭ
            inGame = false; // ���� ����(��ø ���� ����)
            isResetting = true; // ���� ���� (��ø ����)
            StartCoroutine(ResetSetting());
        }

        if (currentPuzzle <= 0) // ���°� ����� ������ ��� Ŭ�����
        {
            PuzzleClear(); // ���� Ŭ����
        }
        else // �������̸�
        {
            door.SetActive(true); // �� Ȱ��ȭ
        }
    }

    void SelectRandomPuzzle(int num) //puzzleNum)��ŭ ���� ���� ����
    {
        currentPuzzle = 1; // ���� �� �ʱ�ȭ (�ڵ� ��ø ����)
        List<int> selectedIndices = new List<int>();

        for (int i = 0; i < puzzleNum; i++)
        {
            int randomIndex;

            // �ߺ��� ���ϵ��� ����
            do
            {
                randomIndex = Random.Range(0, puzzleObj.Length);
                Debug.Log(randomIndex);
            } while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);

            GameObject selectedPuzzle = puzzleObj[randomIndex];

            // ���õ� ����� ���� �� ��ü ���� ����
            Stage5Chack chackScript = selectedPuzzle.GetComponent<Stage5Chack>();
            if (chackScript != null)
            {
                chackScript.SelectPuzzle();
            }

            num++;
        }

        currentPuzzle = num; // ���°� ����� ���� ���� ����
    }
  
    void PuzzleClear() // Ŭ���� �� �� ����, ��ü ���� �ʱ�ȭ
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

    public void Reset() // ��ü ����
    {
        StartCoroutine(ResetSetting());
    }

    IEnumerator ResetSetting()
    {
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
