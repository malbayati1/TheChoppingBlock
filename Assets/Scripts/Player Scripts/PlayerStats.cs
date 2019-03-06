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
	[HideInInspector] public Health health;

	void Awake()
	{
		health = GetComponent<Health>();
		movementSpeed.SetBaseValue(movementSpeedInspector);
        strength.SetBaseValue(strengthInspector);
        maxHealth.SetBaseValue(maxHealthInspector);
	}

	//Just used to set stat values in the inspector
    void OnValidate()
    {
        //Debug.Log("updating the baseValue from " + movementSpeed.value + " to " + movementSpeedInspector);
        movementSpeed.SetBaseValue(movementSpeedInspector);
        strength.SetBaseValue(strengthInspector);
        maxHealth.SetBaseValue(maxHealthInspector);
    }
}
