using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
	public bool isEntrance;

	private House house;

	void Awake()
	{
		GameObject parent = gameObject;
		do
		{
			if(house = parent.GetComponent<House>())
			{
				return;
			}
		} while(parent.transform.parent != null && (parent = parent.transform.parent.gameObject));
		if(!house)
		{
			Debug.Log("HOUSE SCRIPT COULD NOT BE FOUND");
		}
	}

    void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			if(isEntrance)
			{
				house.Enter();
			}
			else
			{
				house.Exit();
			}
		}
	}
}
