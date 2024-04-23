using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthBar;

    public UnityEvent deathEvent;

    [SerializeField] private bool isBoss;
    [SerializeField] private string bossName;

    private void Awake()
    {
        health = maxHealth;
        if (isBoss)
        {
            GameObject bossUI = GameObject.FindGameObjectWithTag("BossUI");
            healthBar = bossUI.GetComponentInChildren<Slider>();
            bossUI.GetComponentInChildren<TextMeshProUGUI>().text = bossName;
        }
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
