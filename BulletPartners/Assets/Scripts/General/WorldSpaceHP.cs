using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceHP : MonoBehaviour
{
    private GameObject cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        transform.LookAt(cam.transform.position);
    }
}
