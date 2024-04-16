using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    public float growthSpeed;

    private void Update()
    {
        transform.localScale = transform.localScale * growthSpeed;
    }
}
