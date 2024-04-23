using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrimReapersIntern : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject scythe;
    public GameObject throwScythe;
    [SerializeField] private float damage;
    private Transform startSytcheTransform;

    [SerializeField] private int bossState;
    private bool spinToPos;

    [Header("GroundPlane")]
    public GameObject planeObject;
    private Vector3 planeNormal;
    private Vector3 planeCenter;
    private float planeSizeX;
    private float planeSizeZ;

    private Vector3 walkPoint;

    [Header("SpinToPos")]
    [SerializeField] private GameObject holdingScythe;
    [SerializeField] private GameObject rotatePoint;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    private bool hasWalkpoint;

    [Header("ThrowSytche")]
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce;
    private bool hasThrow;
    private GameObject[] players;
    private GameObject closestPlayer;

    [Header("ScytheSpin")]
    private GameObject[] scythes;

    private void Awake()
    {
        startSytcheTransform = holdingScythe.transform;

        StartCoroutine(Brain());

        players = GameObject.FindGameObjectsWithTag("Player");

        planeNormal = planeObject.transform.up;

        planeCenter = planeObject.transform.position;

        planeSizeX = planeObject.transform.localScale.x;
        planeSizeZ = planeObject.transform.localScale.z;

        hasWalkpoint = false;
    }

    private void Update()
    {
        if(bossState == 0)
        {
            spinToPos = true;
        }
        else
        {
            spinToPos = false;
        }

        if(bossState == 1)
        {
            StartCoroutine(EnumSytheThrow());
            bossState = 100;
        }
        else
        {
            StopCoroutine(EnumSytheThrow());
        }

        if(bossState == 2)
        {
            StartCoroutine(EnumScytheSpin());
            bossState = 100;
        }
        else
        {
            StopCoroutine(EnumScytheSpin());
        }


        if (spinToPos)
        {
            SpinToPos();
            holdingScythe.transform.position = rotatePoint.transform.position;
            holdingScythe.transform.rotation = rotatePoint.transform.rotation;
        }
        else
        {
            holdingScythe.transform.position = startSytcheTransform.position;
            holdingScythe.transform.rotation = startSytcheTransform.rotation;
        }
    }

    public void SpinToPos()
    {
        if (!hasWalkpoint)
        {
            walkPoint = GetRandomPositionOnPlane();
            hasWalkpoint = true;
        }


        agent.destination = walkPoint;
        transform.Rotate(0, rotateSpeed, 0);

        if (Vector3.Distance(transform.position, walkPoint) <= 3)
        {
            rotateSpeed = 0;
        }
    }

    public void ThrowScythe()
    {
        FindTarget();
        Vector3 _direction = closestPlayer.transform.position - transform.position;
        transform.LookAt(closestPlayer.transform.position + new Vector3(0,2,0));
        GameObject thrownSytche = Instantiate(throwScythe, throwPoint.position, Quaternion.identity);
        thrownSytche.GetComponent<Rigidbody>().AddForce(throwPoint.transform.right * throwForce, ForceMode.Impulse);
        thrownSytche.GetComponent<Bullet>().bulletDamage = damage;
    }

    public void ScytheSpinAround()
    {
        GameObject spinScythe = Instantiate(scythe, throwPoint.position, Quaternion.identity);
        spinScythe.GetComponent<Scythe>().spinAround = true;
        spinScythe.GetComponent<Bullet>().bulletDamage = damage;
        
    }

    Vector3 GetRandomPositionOnPlane()
    {
        // Generate random positions within the bounds of the plane
        float randomX = Random.Range(-planeSizeX / 2f, planeSizeX / 2f);
        float randomZ = Random.Range(-planeSizeZ / 2f, planeSizeZ / 2f);

        // Calculate the random position on the plane
        Vector3 randomPosition = planeCenter +
                                 planeObject.transform.right * randomX +
                                 planeObject.transform.forward * randomZ;

        return randomPosition;
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

    IEnumerator Brain()
    {
        while (true)
        {
            bossState = Random.Range(0, 3);
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator EnumScytheSpin()
    {
        for (int i = 0; i < 20; i++)
        {
            ScytheSpinAround();
            yield return new WaitForSeconds(0.8f);
        }
        

    }

    IEnumerator EnumSytheThrow()
    {
        for (int i = 0; i < 15; i++)
        {
            ThrowScythe();
            yield return new WaitForSeconds(1f);
        }

    }

}


