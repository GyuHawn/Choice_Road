using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Chack : MonoBehaviour
{
    private Stage5 stage5;

    public bool puzzleChack; // ������Ʈ ����

    private Renderer renderer;

    private void Awake()
    {
        stage5 = GameObject.Find("Manager").GetComponent<Stage5>();
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        puzzleChack = true; // ���� �ʱ�ȭ
    }

    void Update()
    {

    }

    public void SelectPuzzle()
    {
        if (renderer != null)
        {
            // ��������� ����
            renderer.material.color = Color.black;
        }

        puzzleChack = false; // ���� ����
    }

    public void TruePuzzle()
    {
        StartCoroutine(ChangePuzzle());
    }

    IEnumerator ChangePuzzle()
    {
        if(renderer != null) 
        {
            renderer.material.color = Color.red; // �÷��̾� �浹�� ���������� ����

            yield return new WaitForSeconds(3f);  // 3���� ���� ������ �ٽ� ������� ����
            puzzleChack = true;

            renderer.material.color = Color.white;

            stage5.currentPuzzle--; // ���� ���������Ʈ ���� ����
        }
    }
}
