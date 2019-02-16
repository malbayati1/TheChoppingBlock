using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float movementSpeedInspector;
    [SerializeField] private float strengthInspector;
    [SerializeField] private float healthInspector;
    [SerializeField] private float maxHealthInspector;
    [HideInInspector] public Stat movementSpeed;
    [HideInInspector] public Stat strength;
    [HideInInspector] public Stat health;
    [HideInInspector] public Stat maxHealth;

    void OnValidate()
    {
        //Debug.Log("updating the baseValue from " + movementSpeed.value + " to " + movementSpeedInspector);
        movementSpeed.SetBaseValue(movementSpeedInspector);
        strength.SetBaseValue(strengthInspector);
        health.SetBaseValue(healthInspector);
        maxHealth.SetBaseValue(maxHealthInspector);
    }

    // public void TakeDamage(int delta)
    // {
    //     health -= delta;
    //     if(health <= 0)
    //     {
    //         Die();
    //     }
    // }

    // public void HealDamage(int delta)
    // {
    //     health += delta;
    //     health = (health > maxHealth) ? maxHealth : health;
    // }

    void Die()
    {
        //DIE
    }
}
