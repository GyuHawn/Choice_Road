using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject[] bridge;
    public int bridgeNum;
    public GameObject functionWall;

    void Start()
    {
        bridgeNum = 0;
    }

    void Update()
    {
        if(bridgeNum != 0)
        {
            StartCoroutine(DestroyedBridge());
        }
    }

    public void SelectBridge()
    {
        bridgeNum = Random.Range(1, 3);
        functionWall.gameObject.SetActive(false);
    }

    IEnumerator DestroyedBridge()
    {
        if (bridgeNum == 1)
        {
            bridge[0].gameObject.SetActive(false);
        }
        else if (bridgeNum == 2)
        {
            bridge[1].gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(3f);

        bridgeNum = 0;
        functionWall.gameObject.SetActive(true);
        foreach (var bridge in bridge)
        {
            bridge.gameObject.SetActive(true);
        }
    }
}
