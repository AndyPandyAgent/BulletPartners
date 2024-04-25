using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class InputHandler : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    [SerializeField] private Rubberband rubberband;
    private S_GameManager gameManager;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private int playerIndex = 0;

    private Vector3 moveDirection;
    private Vector2 movement;
    public bool isFlying;

    [Header("Dodge")]
    [SerializeField] private float dodgeLength;
    [SerializeField] private float iFrameTime;

    public bool hasMouse;
    private Vector3 rotationDirection;
    private Vector2 rotation;
    public Vector2 mouseRot;
    public RaycastHit mouseHit;

    [Header("Gun")]
    public bool shooting;
    public List<GameObject> guns;
    [SerializeField] private GameObject currentGun;
    private int currentGunSlot;
    [SerializeField] private Transform gunSpawn;
    private PlayerUIManager uiManager;

    [Header("Death")]
    [SerializeField] private float respawnTime;
    [SerializeField] private GameObject tombstone;
    private GameObject newTombestone;

    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private List<GameObject> allPlayers;
    [SerializeField] private float maxDist;
    [SerializeField] private float pullSpeed;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        rubberband = FindAnyObjectByType<Rubberband>();
        gameManager = FindAnyObjectByType<S_GameManager>();

        if (objs.Length > 2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        uiManager = GetComponent<PlayerUIManager>();

        FindOtherPlayer();

        Instantiate(guns[currentGunSlot], gunSpawn);
        Invoke(nameof(GetCurrentGun), 0.01f);
    }

    public void SetInputVector(Vector2 direction)
    {
        movement = direction;
    }

    public void SetRotateVector(Vector2 _rotation)
    {
        rotation = _rotation;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void Shoot()
    {
        if (shooting && currentGun != null && !isFlying)
        {
            currentGun.GetComponent<Gun>().Shoot();
        }

    }

    public void SwitchGun()
    {
        if (GetComponentInChildren<Gun>())
        {
            currentGun = GetComponentInChildren<Gun>().gameObject;
            Destroy(currentGun);
        }
        currentGunSlot++;
        if(currentGunSlot > guns.Count - 1)
        {
            currentGunSlot = 0;
        }
        Instantiate(guns[currentGunSlot], gunSpawn);
        Invoke(nameof(GetCurrentGun), 0.1f);

    }

    private void GetCurrentGun()
    {
        Gun gun = GetComponentInChildren<Gun>();
        currentGun = gun.gameObject;
        uiManager.gun = gun;
    }

    private void FindOtherPlayer()
    {
        GameObject[] allPlayersArray = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in allPlayersArray)
        {
            allPlayers.Add(player);
        }

        allPlayers.Remove(gameObject);
        otherPlayer = allPlayers[0];
    }

    ///Dodge functionallity
    public void Dodge()
    {
        if (!isFlying)
        {
            if(Vector3.Distance(transform.position, otherPlayer.transform.position) < rubberband.breakValue)
            {
                SetFlying(0.06f);
            }
            rb.AddForce(moveDirection * dodgeLength, ForceMode.Impulse);
            capsuleCollider.enabled = false;
            rubberband.DodgeLeap(playerIndex);
            if (capsuleCollider.enabled == false)
            {
                Invoke(nameof(IFrameReset), iFrameTime);
            }
        }
    }

    private void IFrameReset()
    {
        capsuleCollider.enabled = true;
    }

    public void SetFlying(float time)
    {
        isFlying = true;
        Invoke(nameof(ResetFlying), time);
    }

    private void ResetFlying()
    {
        isFlying = false;
    }

    public void Death()
    {
        GameObject currentTombstone = Instantiate(tombstone, transform.position, Quaternion.identity);
        currentTombstone.GetComponent<Tombstone>().SetTimer(respawnTime);
        newTombestone = currentTombstone;
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), respawnTime);
        gameManager.playersAlive--;
    }

    private void Respawn()
    {
        Health hp = GetComponent<Health>();

        gameObject.SetActive(true);
        Destroy(newTombestone);
        hp.health = hp.maxHealth;
        hp.hasDied = false;
        gameManager.playersAlive++;
    }

    private void Update()
    {
        if (!isFlying)
        {
            moveDirection = new Vector3(movement.x, 0, movement.y);
            rb.velocity = moveDirection * moveSpeed;
        }


        if (hasMouse)
        {
            Vector3 dir = (mouseHit.point - transform.position);
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(-dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot * Quaternion.Euler(0, 90, 0), Time.deltaTime * 40);
        }
        else
        {
            rotationDirection = new Vector3(-rotation.x, 0, -rotation.y).normalized;

            rotationDirection = Camera.main.transform.TransformDirection(rotationDirection);
            rotationDirection.y = 0;

            Quaternion targetRot = Quaternion.LookRotation(rotationDirection, Vector3.up);

            targetRot *= Quaternion.Euler(0, 90, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 40);
        }

        Shoot();
    }
}
