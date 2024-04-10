using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public bool spinAround;
    public bool moveToBoss;
    public float rotateSpeed;
    private GameObject boss;
    private Vector3 _playerPosition;
    [SerializeField] private float _radius;
    [SerializeField] private float _height;
    [SerializeField] private float returnSpeed;


    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        _radius = 3;
        moveToBoss = false;
    }
    private void Update()
    {
        if (spinAround)
        {
            _radius += 0.003f;
            _playerPosition = _radius * Vector3.Normalize(new Vector3(transform.position.x, _height, transform.position.z) - new Vector3(boss.transform.position.x, 0, boss.transform.position.z)) + boss.transform.position;
            transform.position = _playerPosition;

            transform.RotateAround(boss.transform.position, boss.transform.up, rotateSpeed * Time.deltaTime);
        }
        else if (moveToBoss)
        {
            transform.LookAt(boss.transform);
            transform.position += transform.forward * returnSpeed * Time.deltaTime;
        }
        
        if(boss == null)
        {
            Destroy(gameObject);
        }
    }
}
