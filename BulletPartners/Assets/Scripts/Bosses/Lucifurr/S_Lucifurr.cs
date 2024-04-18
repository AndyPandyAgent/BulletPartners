using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_Lucifurr : MonoBehaviour
{
    private GameObject[] players;
    private GameObject closestPlayer;
    public UnityEvent[] functions;

    [Header("Leap")]
    public bool isGoingUp;
    public bool isGoingDown;
    public GameObject planeObject;
    [SerializeField] private Transform jumpPos;
    [SerializeField] private float returnTimer;
    private float planeSizeX;
    private float planeSizeZ;
    private Vector3 planeCenter;
    private float startingY;
    private Vector3 returnPos;
    private bool hasReturn;

    [Header("Furrball")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject furrball;
    [SerializeField] private float shootForce;
    [SerializeField] private float damage;
    [SerializeField] private float furrballGrowthSpeed;


    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        startingY = transform.position.y;

        planeCenter = planeObject.transform.position;

        planeSizeX = planeObject.transform.localScale.x;
        planeSizeZ = planeObject.transform.localScale.z;

        StartCoroutine(Brain());
    }

    private void Update()
    {
        if(isGoingDown)
            GoingDown();
        if (isGoingUp)
        {
            GoingUp();
            print("UP");
        }
    }

    private void PickRandomEvent()
    {
        int random = Random.Range(0, functions.Length);
        functions[random].Invoke();
        print("RandomEvent");
    }

    private void GoingUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, jumpPos.position, Time.deltaTime * 40);
    }

    private void GoingDown()
    {
        if (!hasReturn)
        {
            returnPos = GetRandomPositionOnPlane();
            hasReturn = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(returnPos.x, startingY, returnPos.z), Time.deltaTime * 40);
    }

    Vector3 GetRandomPositionOnPlane()
    {
        float randomX = Random.Range(-planeSizeX / 2f, planeSizeX / 2f);
        float randomZ = Random.Range(-planeSizeZ / 2f, planeSizeZ / 2f);

        Vector3 randomPosition = planeCenter +
                                 planeObject.transform.right * randomX +
                                 planeObject.transform.forward * randomZ;

        return randomPosition;
    }

    private void Shoot()
    {
        FindTarget();
        transform.LookAt(closestPlayer.transform.position + new Vector3(0, 2, 0));
        GameObject currentFurrball = Instantiate(furrball, shootPoint.position, Quaternion.identity);
        currentFurrball.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * shootForce, ForceMode.Impulse);
        currentFurrball.GetComponent<Bullet>().bulletDamage = damage;
        currentFurrball.GetComponent<Growth>().growthSpeed = furrballGrowthSpeed;
    }

    void FindTarget()
    {

        float lowestDist = Mathf.Infinity;


        for (int i = 0; i < players.Length; i++)
        {

            float dist = Vector3.Distance(players[i].transform.position, transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                closestPlayer = players[i];
            }

        }
    }

    public void InitializeLeap()
    {
        StartCoroutine(Leap());
    }
    public void InitializeFurrball()
    {
        StartCoroutine(Furrball());
    }

    IEnumerator Brain()
    {
        while (true)
        {
            PickRandomEvent();
            print("Brain");
            yield return new WaitForSeconds(10);
        }

    }

    IEnumerator Leap()
    {
        isGoingUp = true;
        isGoingDown = false;
        hasReturn = false;
        yield return new WaitForSeconds(returnTimer);
        isGoingDown = true;
        isGoingUp = false;
    }

    IEnumerator Furrball()
    {
        for (int i = 0; i < 5; i++)
        {
            Shoot();
            yield return new WaitForSeconds(2f);
        }
    }
}
