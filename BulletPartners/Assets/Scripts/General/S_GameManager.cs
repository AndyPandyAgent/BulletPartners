
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameManager : MonoBehaviour
{
    public GameObject[] initialPlayers;
    public List<GameObject> playerList;
    public int playersAlive;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;

    private void Awake()
    {
        initialPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in initialPlayers)
        {
            playerList.Add(player);
        }
        playersAlive = initialPlayers.Length;

        Invoke(nameof(TurnOffUI), 0.001f);
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

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
        loseCanvas.SetActive(false);
        TurnOffUI();
    }

    private void Lose()
    {
        Time.timeScale = 0.0f;
        loseCanvas.SetActive(true);
    }

    private void TurnOffUI()
    {
        loseCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    private void Update()
    {
        if(playersAlive <= 0)
        {
            Lose();
        }
    }

}
