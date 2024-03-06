using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8 : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject player; // ÇÃ·¹ÀÌ¾î

    public GameObject[] redGem; // [0, 1] -> 0 : È¹µæÀü, 1: È¹µæÈÄ
    public bool onRed;
    public GameObject[] greenGem;
    public bool onGreen;
    public GameObject[] blueGem;
    public bool onBlue;

    public GameObject door;

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

        onRed = false;
        onGreen = false;
        onBlue = false;

        door.SetActive(true);
    }


    void Update()
    {
        if (player.GetComponent<Collider>().bounds.Intersects(redGem[0].GetComponent<Collider>().bounds))
        {
            GetRedGem();
        }
        else if (player.GetComponent<Collider>().bounds.Intersects(greenGem[0].GetComponent<Collider>().bounds))
        {
            GetGreenGem();
        }
        else if (player.GetComponent<Collider>().bounds.Intersects(blueGem[0].GetComponent<Collider>().bounds))
        {
            GetBlueGem();
        }

        if(onRed && onGreen && onBlue)
        {
            OpenDoor();
        }
    }

    void GetRedGem()
    {
        redGem[0].SetActive(false);
        redGem[1].SetActive(true);
        onRed = true;
    }

    void GetGreenGem()
    {
        greenGem[0].SetActive(false);
        greenGem[1].SetActive(true);
        onGreen = true;
    }

    void GetBlueGem()
    {
        blueGem[0].SetActive(false);
        blueGem[1].SetActive(true);
        onBlue = true;
    }

    void OpenDoor()
    {
        door.SetActive(false);

        ResetStting();
    }

    void ResetStting()
    {
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(20f);

        redGem[0].SetActive(true);
        redGem[1].SetActive(false);
        greenGem[0].SetActive(true);
        greenGem[1].SetActive(false);
        blueGem[0].SetActive(true);
        blueGem[1].SetActive(false);

        onRed = false;
        onGreen = false;
        onBlue = false;

        door.SetActive(true);
    }
}
