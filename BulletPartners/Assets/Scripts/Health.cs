using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthBar;

    public UnityEvent deathEvent;

    private void Awake()
    {
        health = maxHealth;
    }

    public void SubtractHealth(float amount)
    {
        health -= amount;
    }

    public void AddHealth(float amount)
    {
        health += amount;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        healthBar.value = health / maxHealth;

        if(health <= 0)
        {
            deathEvent.Invoke();
        }
    }

}
