using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private S_Camera cam;

    public float offsetDistance = 3f;
    public GameObject currentEnemy;
    [SerializeField] private float timeBetweenSpawn;


    void Start()
    {
        SpawnObjectAround();
    }

    private void Awake()
    {
        cam = FindAnyObjectByType<S_Camera>();
    }

    private void Update()
    {
        transform.position = new Vector3(cam.center.x, transform.position.y, cam.center.z);
    }

    void SpawnObjectAround()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float xOffset = offsetDistance * Mathf.Cos(angle);
        float zOffset = offsetDistance * Mathf.Sin(angle);

        Vector3 newPosition = transform.position + new Vector3(xOffset, 0f, zOffset);

        GameObject newObject = Instantiate(currentEnemy, newPosition, Quaternion.identity);

        Invoke(nameof(SpawnObjectAround), timeBetweenSpawn);
    }
}
