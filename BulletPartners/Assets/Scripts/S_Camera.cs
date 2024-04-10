using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Camera : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    [SerializeField] private float yModifier;
    [SerializeField] private float zOffset;

    private void Update()
    {
        if (player1 != null && player2 != null)
        {
            Vector3 sum = player1.transform.position + player2.transform.position;
            Vector3 center = sum / 2;

            float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
            distance = distance * yModifier;

            if (distance < minY)
            {
                distance = minY;
            }
            else if (distance > maxY)
            {
                distance = maxY;
            }

            transform.position = Vector3.Lerp(transform.position ,new Vector3(center.x, distance, center.z + zOffset), Time.deltaTime * 4);
            transform.LookAt(center);
        }

        if(player1 != null && player2 == null)
        {
            transform.position = new Vector3(player1.transform.position.x, minY, player1.transform.position.z + zOffset);
            transform.LookAt(player1.transform.position);
        }
        else if (player1 == null && player2 != null)
        {
            transform.position = new Vector3(player2.transform.position.x, minY, player2.transform.position.z + zOffset);
            transform.LookAt(player2.transform.position);
        }

    }
}
