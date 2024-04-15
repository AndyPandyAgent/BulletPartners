using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class S_Camera : MonoBehaviour
{
    [HideInInspector] public GameObject[] initialPlayers;
    [HideInInspector] public List<GameObject> playerList;

    [SerializeField] private Vector3 center;

    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    [SerializeField] private float yModifier;
    [SerializeField] private float zOffset;

    private Vector3 shakeVector;
    private bool isShaking;
    private void Awake()
    {
        initialPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in initialPlayers)
        {
            playerList.Add(player);
        }
        isShaking = false;
    }

    private void Update()
    {
        print(playerList.Count);

        if (playerList.Count > 1)
        {
            Vector3 sum = playerList[0].transform.position + playerList[1].transform.position;

            if (!isShaking)
            {
                center = sum / 2;
            }

            float distance = Vector3.Distance(playerList[0].transform.position, playerList[1].transform.position);
            distance = distance * yModifier;

            if (distance < minY)
            {
                distance = minY;
            }
            else if (distance > maxY)
            {
                distance = maxY;
            }



            transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, distance, center.z + zOffset), Time.deltaTime * 100);

        }

        if(playerList.Count == 1)
        {
            if (!isShaking)
            {
                center = playerList[0].transform.position;
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, minY, center.z + zOffset), Time.deltaTime * 100);
        }

    }

    public void ScreenShake(Vector3 dir, Transform trans, float forceMultiplier)
    {
        isShaking = true;
        center += -dir * forceMultiplier + new Vector3(Random.Range(0, 0.3f), 0, Random.Range(0,0.3f));
        Invoke(nameof(ShakeVectorReset), 0.1f);
    }

    private void ShakeVectorReset()
    {
        isShaking = false;
    }
}
