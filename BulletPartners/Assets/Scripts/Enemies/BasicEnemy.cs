using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private S_GameManager gameManager;

    private Rigidbody rb;

    private List<GameObject> players;
    private GameObject closestPlayer;

    [Header("Values")]
    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<S_GameManager>();
    }

    private void Update()
    {
        players = gameManager.playerList;

        GetClosestPlayer();

        rb.velocity = transform.position - closestPlayer.transform.position * movementSpeed;
    }

    private void GetClosestPlayer()
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
}
