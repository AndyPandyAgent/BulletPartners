using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime;
    public float bulletDamage;
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
        if(other.tag == "Player" || other.tag == "Boss")
        {
            other.GetComponent<Health>().SubtractHealth(bulletDamage);
            print(other.GetComponent<Health>().health);
            DestroySelf();
        }
    }
}
