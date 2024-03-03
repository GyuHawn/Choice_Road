using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Chack : MonoBehaviour
{
    private Stage5 stage5;

    public bool puzzleChack;

    private void Awake()
    {
        stage5 = GameObject.Find("Manager").GetComponent<Stage5>();
    }

    void Start()
    {
        puzzleChack = true;
    }

    void Update()
    {

    }

    // 2. 선택된 puzzleObj는 색을 노란색으로 변경하고 나머지 puzzleObj의 puzzleChack를 참으로 변경하는 메서드
    public void SelectPuzzle()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // 색을 노란색으로 변경
            renderer.material.color = Color.yellow;
        }

        puzzleChack = false;
    }

    public void TruePuzzle()
    {
        StartCoroutine(ChangePuzzle());
    }

    IEnumerator ChangePuzzle()
    {
        yield return new WaitForSeconds(3f);
        puzzleChack = true;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white;
        }
        stage5.currentPuzzle--;
    }
}
