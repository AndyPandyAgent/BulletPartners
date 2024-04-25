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
    [HideInInspector] public bool hasDied;

    public UnityEvent deathEvent;

    [SerializeField] private bool isBoss;
    [SerializeField] private string bossName;

    private void Awake()
    {
        health = maxHealth;
        hasDied = false;
        if (isBoss)
        {
            GameObject bossUI = GameObject.FindGameObjectWithTag("BossUI");
            healthBar = bossUI.GetComponentInChildren<Slider>();
            bossUI.GetComponentInChildren<TextMeshProUGUI>().text = bossName;

            deathEvent.AddListener(() => FindAnyObjectByType<S_GameManager>().FinishRound());
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

        if(health <= 0 && !hasDied)
        {
            deathEvent.Invoke();
            hasDied = true;
        }
    }

}
