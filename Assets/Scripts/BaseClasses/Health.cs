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
                return Mathf.RoundToInt(playerStats.maxHealth.Value);
            }
            else
            {
                return maxHealthInspector;
            }
        }
    }

	public delegate void HealthChangeDelegate(int delta);
	public event HealthChangeDelegate healEvent = delegate {};
	public event HealthChangeDelegate damageEvent = delegate {};
	public event HealthChangeDelegate changeEvent = delegate {};

    // Might be null- determines whether to get maxhealth from playerstats
    private PlayerStats playerStats;

    void Awake()
    {
		playerStats = GetComponent<PlayerStats>();
        health = maxHealth;
    }

    public virtual void Damage(int amount)
    {
		Debug.Log("taking " + amount + " damage!");
        health -= amount;
		Debug.Log("HP:"+health+"/"+maxHealth);
		damageEvent(amount);
		changeEvent(-amount);

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        health += amount;
		healEvent(amount);
		changeEvent(amount);

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
