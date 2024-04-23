using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private GameObject[] inputs;

    private void Update()
    {
        inputs = GameObject.FindGameObjectsWithTag("Input");

        if(inputs.Length != 2)
        {
            
        }
    }
}
