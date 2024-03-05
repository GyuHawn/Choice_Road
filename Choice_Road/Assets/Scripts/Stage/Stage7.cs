using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Stage7 : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject player; // 플레이어
    public GameObject[] success;
    public GameObject[] fail1;
    public GameObject[] fail2;

    public GameObject startWall;

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    void Update()
    {
        if (stageManager.stageNum == 7)
        {
            if (player.GetComponent<Collider>().bounds.Intersects(success[0].GetComponent<Collider>().bounds))
            {
                SuccessFloor();
                startWall.SetActive(true);
            }
            else if (player.GetComponent<Collider>().bounds.Intersects(fail1[0].GetComponent<Collider>().bounds))
            {
                Fail1Floor();
                startWall.SetActive(true);
            }
            else if (player.GetComponent<Collider>().bounds.Intersects(fail2[0].GetComponent<Collider>().bounds))
            {
                Fail2Floor();
                startWall.SetActive(true);
            }
        }
    }

    void SuccessFloor()
    {
        foreach (var failObject in fail1)
        {
            failObject.SetActive(false);
        }

        foreach (var failObject in fail2)
        {
            failObject.SetActive(false);
        }

        StartCoroutine(OnSuccessObjects());
    }

    IEnumerator OnSuccessObjects()
    {
        for (int i = 0; i < success.Length; i++)
        {
            success[i].SetActive(true);
            yield return new WaitForSeconds(2f);
            if(i > 0)
            {
                success[i - 1].SetActive(false);
            }
        }

        StartCoroutine(ResetStage());
    }

    void Fail1Floor()
    {
        foreach (var seccessObject in success)
        {
            seccessObject.SetActive(false);
        }

        foreach (var failObject in fail2)
        {
            failObject.SetActive(false);
        }

        StartCoroutine(OnFail1Objects());
    }

    IEnumerator OnFail1Objects()
    {
        for (int i = 0; i < fail1.Length; i++)
        {
            fail1[i].SetActive(true);
            yield return new WaitForSeconds(2f);
            if (i > 0)
            {
                fail1[i - 1].SetActive(false);
            }
        }

        StartCoroutine(ResetStage());
    }

    void Fail2Floor()
    {
        foreach (var seccessObject in success)
        {
            seccessObject.SetActive(false);
        }

        foreach (var failObject in fail1)
        {
            failObject.SetActive(false);
        }

        StartCoroutine(OnFail2Objects());
    }

    IEnumerator OnFail2Objects()
    {
        for (int i = 0; i < fail2.Length; i++)
        {
            fail2[i].SetActive(true);
            yield return new WaitForSeconds(2f);
            if (i > 0)
            {
                fail2[i - 1].SetActive(false);
            }
        }

        StartCoroutine(ResetStage());
    }

    IEnumerator ResetStage()
    {
        yield return new WaitForSeconds(5f);

        foreach (var seccessObject in success)
        {
            seccessObject.SetActive(false);
        }

        foreach (var failObject in fail1)
        {
            failObject.SetActive(false);
        }

        foreach (var failObject in fail2)
        {
            failObject.SetActive(false);
        }

        success[0].SetActive(true);
        fail1[0].SetActive(true);
        fail2[0].SetActive(true);
        startWall.SetActive(false);
    }
}
