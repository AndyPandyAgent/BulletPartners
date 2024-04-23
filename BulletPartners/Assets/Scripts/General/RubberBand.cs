using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rubberband : MonoBehaviour
{
    private Rigidbody p1rb;
    private Rigidbody p2rb;
    private S_GameManager gameManager;

    public LineRenderer lineRenderer;
    private Material lineColor;
    private List<GameObject> playerList;

    [Header("PullValues")]
    [SerializeField] private float pullForce;
    [SerializeField] private float leapForce;
    [SerializeField] private float dragValue;
    private bool hasStartedLeaping;
    private float distanceBetweenPlayers;

    [Header("LineColors")]
    [SerializeField] private Material breakColor;
    [SerializeField] private Material normalColor;

    [Header("FunctionMarkers")]
    [SerializeField] private float breakValue;
    [SerializeField] private float breakTimer;
    private float timer;
    private bool hasBeginBreak;
    private bool hasFoundDistance;

    [Header("BreakValues")]
    [SerializeField] private float concussionTime;


    private void Awake()
    {
        gameObject.SetActive(true);
        gameManager = FindAnyObjectByType<S_GameManager>();
        lineRenderer = GetComponent<LineRenderer>();
        playerList = gameManager.playerList;

        p1rb = playerList[0].GetComponent<Rigidbody>();
        p2rb = playerList[1].GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerList = gameManager.playerList;

        lineRenderer.SetPosition(0, playerList[0].transform.position);
        lineRenderer.SetPosition(1, playerList[1].transform.position);

        lineRenderer.material = lineColor;

        distanceBetweenPlayers = Vector3.Distance(playerList[0].transform.position, playerList[1].transform.position);

        if (playerList[1] != null)
        {
            if (distanceBetweenPlayers > breakValue)
            {
                p1rb.AddForce((playerList[1].transform.position - playerList[0].transform.position) * pullForce);
                p2rb.AddForce((playerList[0].transform.position - playerList[1].transform.position) * pullForce);
            }
        }

        if (distanceBetweenPlayers >= breakValue)
        {
            StartBreakLine();
            lineColor = breakColor;
        }
        else
        {
            hasBeginBreak = false;
            lineColor = normalColor;
        }
    }

    private void StartBreakLine()
    {
        if (!hasBeginBreak)
        {
            timer = 0f;
            Invoke(nameof(StartBreakTimer), 0.1f);
        }
        else
        {
            timer += 0.004f;

            if (timer >= breakTimer)
            {
                BreakLine();
            }
        }
    }

    private void StartBreakTimer()
    {
        hasBeginBreak = true;
    }

    private void BreakLine()
    {
        gameObject.SetActive(false);

        foreach (var player in playerList)
        {
            player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            player.GetComponent<InputHandler>().isFlying = true;
        }

        Invoke(nameof(BreakReset), concussionTime);
    }

    public void DodgeLeap(int index)
    {
        if (index == 0 && distanceBetweenPlayers > breakValue)
        {
            p2rb.AddForce((playerList[1].transform.position - playerList[0].transform.position) * leapForce, ForceMode.Impulse);
            p1rb.AddForce((playerList[1].transform.position - playerList[0].transform.position) * leapForce, ForceMode.Impulse);
            playerList[1].GetComponent<InputHandler>().isFlying = true;
            playerList[0].GetComponent<InputHandler>().isFlying = true;
            Invoke(nameof(DodgeLeapReset), 0.6f);
        }
    }

    private void DodgeLeapReset()
    {
        playerList[1].GetComponent<InputHandler>().isFlying = false;
        playerList[0].GetComponent<InputHandler>().isFlying = false;
    }

    private void BreakReset()
    {
        gameObject.SetActive(true);
        foreach (var player in playerList)
        {
            player.GetComponent<InputHandler>().isFlying = false;
        }
    }
}
