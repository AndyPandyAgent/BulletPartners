using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_Lucifurr : MonoBehaviour
{
    [SerializeField] private S_GameManager gameManager;

    private List <GameObject> players;
    private GameObject closestPlayer;
    public UnityEvent[] functions;
    private Rigidbody rb;

    [Header("Leap")]
    public bool isGoingUp;
    public bool isGoingDown;
    public GameObject planeObject;
    [SerializeField] private Transform jumpPos;
    [SerializeField] private float returnTimer;
    [SerializeField] private GameObject landMarker;
    [SerializeField] private int bulletAmount;
    [SerializeField] private GameObject bulletPrefab;
    private float startingY;
    [SerializeField] private Vector3 returnPos;
    private bool hasReturn;
    [SerializeField] private float lilDamage;

    [Header("Furrball")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject furrball;
    [SerializeField] private float shootForce;
    [SerializeField] private float damage;
    [SerializeField] private float furrballGrowthSpeed;

    [Header("Dash")]
    [SerializeField] private float dashForce;
    private bool isDashing;


    private void Awake()
    {
        gameManager = FindAnyObjectByType<S_GameManager>();
        rb = GetComponent<Rigidbody>();

        startingY = transform.position.y;

        hasReturn = false;

        StartCoroutine(Brain());
    }

    private void Update()
    {
        players = gameManager.playerList;

        if(isGoingDown)
            GoingDown();
        if (isGoingUp)
        {
            GoingUp();
            print("UP");
        }

        if (isDashing)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestPlayer.transform.position, 2 * Time.deltaTime);
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
        if (!hasReturn)
        {
            returnPos = GetRandomPlayer();
            Instantiate(landMarker, returnPos, Quaternion.identity);
            hasReturn = true;
        }
    }

    private void GoingDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(returnPos.x, startingY, returnPos.z), Time.deltaTime * 40);
    }

    private void Landed()
    {
        float angleStep = 360f / bulletAmount;
        float angle = 0f;

        for (int i = 0; i < bulletAmount; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float bulletDirZ = transform.position.z + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 bulletVector = new Vector3(bulletDirX, transform.position.y, bulletDirZ);
            Vector3 bulletDir = (bulletVector - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0,-3,0), Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = bulletDir * 10f;
            bullet.GetComponent<Bullet>().bulletDamage = lilDamage;


            angle += angleStep;
            print("PEW");
        }
    }

    Vector3 GetRandomPlayer()
    {
        int randomNumber = Random.Range(0, players.Count);

        Vector3 randomPos = players[randomNumber].gameObject.transform.position;

        return randomPos;
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


        for (int i = 0; i < players.Count; i++)
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

    public void InitializeDash()
    {
        StartCoroutine(Dash());
        print("Initialze");
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
        yield return new WaitForSeconds(returnTimer);
        isGoingDown = true;
        isGoingUp = false;
        hasReturn = false;
        yield return new WaitForSeconds(0.5f);
        Landed();
    }

    IEnumerator Furrball()
    {
        for (int i = 0; i < 5; i++)
        {
            Shoot();
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Dash()
    {
        print("Enum");
        FindTarget();
        transform.LookAt(closestPlayer.transform.position);
        yield return new WaitForSeconds(1);
        rb.velocity = (closestPlayer.transform.position - transform.position) * 6;
        yield return new WaitForSeconds(2);
        rb.velocity = new Vector3(0,0,0);
    }
}
