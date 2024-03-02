using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4 : MonoBehaviour
{
    public GameObject mazeFloor;
    public GameObject player; // 플레이어

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }


    public void MazeTrap()
    {
        StartCoroutine(OffFloor());
    }

    IEnumerator OffFloor()
    {
        mazeFloor.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        mazeFloor.GetComponent<BoxCollider>().enabled = true;
    }
}
