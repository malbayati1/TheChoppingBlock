using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeEventData
{
	public int delta;
	public bool cancelled;
	public HealthChangeEventData(int d)
	{
		delta = d;
		cancelled = false;
	}
}

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

	public delegate void HealthChangeEffectabledDelegate(HealthChangeEventData hced);
	public delegate void HealthChangeNotifyDelegate(int delta);
	public event HealthChangeEffectabledDelegate preHealEvent = delegate {};
	public event HealthChangeEffectabledDelegate preDamageEvent = delegate {};
	public event HealthChangeNotifyDelegate postDamageEvent = delegate {};
	public event HealthChangeNotifyDelegate postHealEvent = delegate {};
	public event HealthChangeNotifyDelegate changeEvent = delegate {};

    // Might be null- determines whether to get maxhealth from playerstats
    private PlayerStats playerStats;

    void Awake()
    {
		playerStats = GetComponent<PlayerStats>();
        health = maxHealth;
    }

	void OnEnable()
	{
		if(playerStats)
		{
			playerStats.maxHealth.statChangeEvent += ValidateHealth;
		}
	}

	void OnDisable()
	{
		if(playerStats)
		{
			playerStats.maxHealth.statChangeEvent -= ValidateHealth;
		}
	}

	void ValidateHealth(float value) //needs to take a value to subscribe to the stat change event
	{
		if(health > maxHealth)
		{
			health = maxHealth;
		}
	}

    public virtual void Damage(int amount)
    {
		HealthChangeEventData hced = new HealthChangeEventData(-amount);
		preDamageEvent(hced);
		int delta = (hced.cancelled) ? 0 : hced.delta;
        health += delta;
		postDamageEvent(delta);
		changeEvent(delta);

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        HealthChangeEventData hced = new HealthChangeEventData(amount);
		preHealEvent(hced);
		int delta = (hced.cancelled) ? 0 : hced.delta;
        health += delta;
		postHealEvent(delta);
		changeEvent(delta);

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
