using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage10 : MonoBehaviour
{
    public GameObject player; // 플레이어

    public GameObject moveFloor;
    public GameObject startButton;
    public bool stage10Start;

    public GameObject forwardButton;
    public GameObject backButton;
    public GameObject rightButton;
    public GameObject leftButton;

    public float moveSpd;

    public GameObject FinshPoint;
    public GameObject resetPoint;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        stage10Start = false;
        moveSpd = 3f;
    }

    void Update()
    {
        if (!stage10Start)
        {
            startButton.SetActive(true);
            forwardButton.SetActive(false);
            backButton.SetActive(false);
            rightButton.SetActive(false);
            leftButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            forwardButton.SetActive(true);
            backButton.SetActive(true);
            rightButton.SetActive(true);
            leftButton.SetActive(true);
        }

        if (player.GetComponent<Collider>().bounds.Intersects(startButton.GetComponent<Collider>().bounds))
        {
            stage10Start = true;
        }

        if (stage10Start)
        {
            if (player.GetComponent<Collider>().bounds.Intersects(forwardButton.GetComponent<Collider>().bounds))
            {
                FloorForward();
            }
            else if (player.GetComponent<Collider>().bounds.Intersects(backButton.GetComponent<Collider>().bounds))
            {
                FloorBack();
            }
            else if (player.GetComponent<Collider>().bounds.Intersects(rightButton.GetComponent<Collider>().bounds))
            {
                FloorRight();
            }
            else if (player.GetComponent<Collider>().bounds.Intersects(leftButton.GetComponent<Collider>().bounds))
            {
                FloorLeft();
            }
        }

        if (moveFloor.GetComponent<Collider>().bounds.Intersects(FinshPoint.GetComponent<Collider>().bounds))
        {
            ResetSetting();
        }
    }

    void FloorForward()
    {
        moveFloor.transform.position += Vector3.back * moveSpd * Time.deltaTime;
        player.transform.position += Vector3.back * moveSpd * Time.deltaTime;
    }

    void FloorBack()
    {
        moveFloor.transform.position += Vector3.forward * moveSpd * Time.deltaTime;
        player.transform.position += Vector3.forward * moveSpd * Time.deltaTime;
    }

    void FloorRight()
    {
        moveFloor.transform.position += Vector3.left * moveSpd * Time.deltaTime;
        player.transform.position += Vector3.left * moveSpd * Time.deltaTime;
    }

    void FloorLeft()
    {
        moveFloor.transform.position += Vector3.right * moveSpd * Time.deltaTime;
        player.transform.position += Vector3.right * moveSpd * Time.deltaTime;
    }

    public void ResetSetting()
    {
        moveFloor.transform.position = resetPoint.transform.position;
        stage10Start = false;
    }
}
