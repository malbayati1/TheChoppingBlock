using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float movementSpeed;
    public float strength;
    public float health;
    public float maxHealth;

    public void TakeDamage(int delta)
    {
        health -= delta;
        if(health <= 0)
        {
            Die();
        }
    }

    public void HealDamage(int delta)
    {
        health += delta;
        health = (health > maxHealth) ? maxHealth : health;
    }

    void Die()
    {
        //DIE
    }
}
