using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private GameObject[] inputs;
    [SerializeField] private GameObject InputUI;

    private void Awake()
    {
        InputUI.SetActive(true);
    }

    private void Update()
    {
        inputs = GameObject.FindGameObjectsWithTag("Input");

        if(inputs.Length == 2)
        {
            InputUI.SetActive(false);
        }
    }
}
