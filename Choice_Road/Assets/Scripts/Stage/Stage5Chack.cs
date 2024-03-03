using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Chack : MonoBehaviour
{
    private Stage5 stage5;

    public bool puzzleChack; // 오브젝트 상태

    private Renderer renderer;

    private void Awake()
    {
        stage5 = GameObject.Find("Manager").GetComponent<Stage5>();
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        puzzleChack = true; // 상태 초기화
    }

    void Update()
    {

    }

    public void SelectPuzzle()
    {
        if (renderer != null)
        {
            // 노란색으로 변경
            renderer.material.color = Color.black;
        }

        puzzleChack = false; // 상태 변경
    }

    public void TruePuzzle()
    {
        StartCoroutine(ChangePuzzle());
    }

    IEnumerator ChangePuzzle()
    {
        if(renderer != null) 
        {
            renderer.material.color = Color.red; // 플레이어 충돌시 빨간색으로 변경

            yield return new WaitForSeconds(3f);  // 3초후 상태 변경후 다시 흰색으로 변경
            puzzleChack = true;

            renderer.material.color = Color.white;

            stage5.currentPuzzle--; // 남은 퍼즐오브젝트 갯수 감소
        }
    }
}
