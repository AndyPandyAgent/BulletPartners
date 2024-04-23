using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDani : MonoBehaviour
{
    private List<GameObject> playerList;
    private S_GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<S_GameManager>();
    }
    private void Update()
    {
        playerList = gameManager.playerList;

        transform.LookAt(playerList[1].transform.position);
    }
}
