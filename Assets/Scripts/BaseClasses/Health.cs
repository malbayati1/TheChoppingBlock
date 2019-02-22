using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public int health;
    [SerializeField] private int maxHealthInspector;
    [HideInInspector] public int maxHealth
    {
        get 
        {
            if (playerStats)
            {
                return Mathf.RoundToInt(playerStats.maxHealth.value);
            }
            else
            {
                return maxHealthInspector;
            }
        }
    }

    // Might be null- determines whether to get maxhealth from playerstats
    private PlayerStats playerStats;

    void Awake()
    {
        health = maxHealth;

        playerStats = GetComponent<PlayerStats>();
    }

    public virtual void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
