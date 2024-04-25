using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime;
    public float bulletDamage;
    [SerializeField] private string[] tags;
    private void Awake()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in tags)
        {
            if(other.tag == tag)
            {
                other.GetComponent<Health>().SubtractHealth(bulletDamage);
                print(other.GetComponent<Health>().health);
                DestroySelf();
            }
        }
        if(other.tag == "Sheild")
        {
            DestroySelf();
        }
    }
}
