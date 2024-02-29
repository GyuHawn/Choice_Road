using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject player; // 플레이어 

    public GameObject thornFloor;
    public GameObject thornTrap;
    public bool onThorn;
    public GameObject hammerFloor;
    public GameObject hammerTrap;
    public bool onHammer;

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

        onThorn = false;
        onHammer = false;
    }

    void Update()
    {
        // 플레이어와 함정 충돌 감지
        if (!onThorn && player != null && thornFloor != null && player.GetComponent<Collider>().bounds.Intersects(thornFloor.GetComponent<Collider>().bounds))
        {
            onThorn = true;
            Thorn();
        }
        if (!onHammer && player != null && hammerFloor != null && player.GetComponent<Collider>().bounds.Intersects(hammerFloor.GetComponent<Collider>().bounds))
        {
            onHammer = true;
            Hammer();
        }
    }

    void Thorn()
    {
        thornTrap.SetActive(true);

        StartCoroutine(MoveTrap(thornTrap.transform, new Vector3(0, 2.3f, 0), 0.5f)); // 빠르게 올리기

        StartCoroutine(DelayedMoveTrap(thornTrap.transform, new Vector3(0, 1.5f, 0), 3f)); // 3초 후 천천히 내리기
    
        StartCoroutine(DeactivateTrap(thornTrap, 6f, onThorn)); // 3초 후 비활성화
    }

    IEnumerator MoveTrap(Transform trapTransform, Vector3 targetPosition, float duration)
    {
        Vector3 initialPosition = trapTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float yPosition = Mathf.Lerp(initialPosition.y, targetPosition.y, elapsedTime / duration);
            trapTransform.position = new Vector3(initialPosition.x, yPosition, initialPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        trapTransform.position = new Vector3(initialPosition.x, targetPosition.y, initialPosition.z);
    }


    IEnumerator DelayedMoveTrap(Transform trapTransform, Vector3 targetPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(MoveTrap(trapTransform, targetPosition, 2f)); // 2초 동안 천천히 이동
    }


    void Hammer()
    {
        hammerTrap.SetActive(true);

        StartCoroutine(RotateTrap(hammerTrap.transform, new Vector3(0, 0, -90), 0.2f)); // 빠르게 회전하기

        StartCoroutine(DelayedRotateTrap(hammerTrap.transform, Vector3.zero, 2f)); // 2초 후 천천히 원래대로 돌리기

        StartCoroutine(DeactivateTrap(hammerTrap, 6f, onHammer)); // 4초 후 비활성화
    }

    IEnumerator RotateTrap(Transform trapTransform, Vector3 targetRotation, float duration)
    {
        Quaternion initialRotation = trapTransform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            trapTransform.rotation = Quaternion.Lerp(initialRotation, Quaternion.Euler(targetRotation), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        trapTransform.rotation = Quaternion.Euler(targetRotation);
    }

    IEnumerator DelayedRotateTrap(Transform trapTransform, Vector3 targetRotation, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(RotateTrap(trapTransform, targetRotation, 2f)); // 2초 동안 천천히 회전
    }

    IEnumerator DeactivateTrap(GameObject trapObject, float delay, bool on)
    {
        yield return new WaitForSeconds(delay);
        trapObject.SetActive(false);
        on = false;
    }
}


