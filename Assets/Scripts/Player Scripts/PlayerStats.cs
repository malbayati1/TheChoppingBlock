using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float movementSpeedInspector;
    [SerializeField] private float strengthInspector;
    [SerializeField] private float maxHealthInspector;
    [HideInInspector] public Stat movementSpeed;
    [HideInInspector] public Stat strength;
    [HideInInspector] public Stat maxHealth;

	//Just used to set stat values in the inspector
	//Should decide if we want the player health stuff done in here
    void OnValidate()
    {
        //Debug.Log("updating the baseValue from " + movementSpeed.value + " to " + movementSpeedInspector);
        movementSpeed.SetBaseValue(movementSpeedInspector);
        strength.SetBaseValue(strengthInspector);
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

}
