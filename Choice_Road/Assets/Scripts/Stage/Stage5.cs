using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : MonoBehaviour
{
    public GameObject player; // 플레이어
    public GameObject puzzleStart; // 퍼즐 시작 지점
    public bool inGame; // 게임 중인가
    public GameObject[] puzzleObj; // 퍼즐 오브젝트
    public int puzzleNum; // 상태 변경할 오브젝트 개수
    public int currentPuzzle; // 남은 상태가 변경된 오브젝트
    public bool puzzleClear; // 게임 클리어
    public GameObject door; // 문

    public bool isResetting = false; // 리셋 가능한가

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        // 기본 상태 초기화
        inGame = false;
        puzzleNum = 10;
        currentPuzzle = 1;
        isResetting = false;
    }

    void Update()
    {
        // 게임중 x, 플레이어 존재, 플레이어가 퍼즐 시작지점을 충돌시 게임 시작
        if (!inGame && player != null && puzzleStart != null && player.GetComponent<Collider>().bounds.Intersects(puzzleStart.GetComponent<Collider>().bounds))
        {
            inGame = true;
            puzzleStart.SetActive(false);
        }

        // 게임중 o, 리셋불가
        if (inGame && !isResetting)
        {
            SelectRandomPuzzle(0); // 랜덤 퍼즐 선택
            puzzleStart.SetActive(false); // 시작 지점 비활성화
            inGame = false; // 게임 중지(중첩 시작 방지)
            isResetting = true; // 리셋 가능 (중첩 방지)
            StartCoroutine(ResetSetting());
        }

        if (currentPuzzle <= 0) // 상태가 변경된 퍼즐을 모두 클리어시
        {
            PuzzleClear(); // 게임 클리어
        }
        else // 게임중이면
        {
            door.SetActive(true); // 문 활성화
        }
    }

    void SelectRandomPuzzle(int num) //puzzleNum)만큼 랜덤 퍼즐 선택
    {
        currentPuzzle = 1; // 기초 값 초기화 (코드 중첩 방지)
        List<int> selectedIndices = new List<int>();

        for (int i = 0; i < puzzleNum; i++)
        {
            int randomIndex;

            // 중복값 피하도록 설정
            do
            {
                randomIndex = Random.Range(0, puzzleObj.Length);
                Debug.Log(randomIndex);
            } while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);

            GameObject selectedPuzzle = puzzleObj[randomIndex];

            // 선택된 퍼즐색 변경 및 전체 상태 변경
            Stage5Chack chackScript = selectedPuzzle.GetComponent<Stage5Chack>();
            if (chackScript != null)
            {
                chackScript.SelectPuzzle();
            }

            num++;
        }

        currentPuzzle = num; // 상태가 변경된 퍼즐 개수 설정
    }
  
    void PuzzleClear() // 클리어 시 문 열기, 전체 상태 초기화
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

    public void Reset() // 전체 리셋
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
