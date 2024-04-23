using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private float knockbackForce;
    [SerializeField] private float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Attack(collision.gameObject, damage);
        }
    }

    private void Attack(GameObject player, float damage)
    {
        player.GetComponent<Health>().SubtractHealth(damage);
        player.GetComponent<Rigidbody>().AddForce(transform.forward * knockbackForce, ForceMode.Impulse);
    }
}
