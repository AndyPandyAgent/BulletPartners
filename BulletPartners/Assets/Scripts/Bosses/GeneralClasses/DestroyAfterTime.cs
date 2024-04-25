using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float time;

    private void Awake()
    {
        Invoke(nameof(DestroytSelf), time);
    }
    
    private void DestroytSelf()
    {
        Destroy(gameObject);
    }
}
