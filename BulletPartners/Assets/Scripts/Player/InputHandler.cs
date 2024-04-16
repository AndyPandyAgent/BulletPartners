using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class InputHandler : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    public Rubberband rubberband;

>>>>>>> Stashed changes
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private int playerIndex = 0;

    private Vector3 moveDirection;
    private Vector2 movement;

<<<<<<< Updated upstream
=======
    [Header("Dodge")]
    [SerializeField] private float dodgeLength;
    [SerializeField] private float iFrameTime;
    public bool isFlying;

>>>>>>> Stashed changes
    public bool hasMouse;
    private Vector3 rotationDirection;
    private Vector2 rotation;
    public Vector2 mouseRot;
    public RaycastHit mouseHit;

<<<<<<< Updated upstream
    private Rigidbody rb;

=======
>>>>>>> Stashed changes
    [Header("Gun")]
    public bool shooting;
    public List<GameObject> guns;
    [SerializeField] private GameObject currentGun;
    private int currentGunSlot;
    [SerializeField] private Transform gunSpawn;
    private PlayerUIManager uiManager;

    [SerializeField] private List<GameObject> allPlayers;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        uiManager = GetComponent<PlayerUIManager>();
        isFlying = false;

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
        if (shooting && currentGun != null)
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

    }

<<<<<<< Updated upstream
=======
    ///Dodge functionallity
    public void Dodge()
    {
        rb.AddForce(moveDirection * dodgeLength, ForceMode.Impulse);
        capsuleCollider.enabled = false;
        rubberband.DodgeLeap(playerIndex);
        if(capsuleCollider.enabled == false)
        {
            Invoke(nameof(IFrameReset), iFrameTime);
        }
    }

    private void IFrameReset()
    {
        capsuleCollider.enabled = true;
    }

>>>>>>> Stashed changes
    private void Update()
    {
        moveDirection = new Vector3(movement.x, 0, movement.y);
        if (!isFlying)
        {
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchGun();
        }
        Shoot();
    }
}
