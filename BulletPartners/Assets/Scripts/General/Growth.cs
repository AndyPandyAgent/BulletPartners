using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    public float growthSpeed;
    public float maxGrowth;

    private void Update()
    {
        if(transform.localScale.y < maxGrowth)
            transform.localScale = transform.localScale * growthSpeed;
    }
}
