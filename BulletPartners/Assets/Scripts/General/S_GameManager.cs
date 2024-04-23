using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameManager : MonoBehaviour
{
    public GameObject[] initialPlayers;
    public List<GameObject> playerList;
    [SerializeField] private GameObject winCanvas;

    private void Awake()
    {
        winCanvas.SetActive(false);
        initialPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in initialPlayers)
        {
            playerList.Add(player);
        }
    }

    public void FinishRound()
    {
        winCanvas.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        winCanvas.SetActive(false);
    }
}
