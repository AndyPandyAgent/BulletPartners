using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        transform.Rotate(rotationSpeed, 0, 0);
    }
}
