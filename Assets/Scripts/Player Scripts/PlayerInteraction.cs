using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private GameObject heldItem;
	private IHoldable heldItemInteraction;

    void Start()
    {
        
    }

    void Update()
    {
		if(heldItem != null)
		{
			UpdateHoldablePosition();
			if(Input.GetButtonDown("Use"))
			{
				heldItemInteraction.Use(gameObject);
				ClearFields();
			}
			else if(Input.GetButtonDown("Drop"))
			{
				heldItemInteraction.Drop(gameObject);
				ClearFields();
			}
		}
		
		if(Input.GetButtonDown("Use"))
		{
		}
		if(Input.GetButtonDown("Drop"))
		{
		}
    }

	void UpdateHoldablePosition()
	{
		heldItem.transform.position = transform.position;
	}

	void ClearFields()
	{
		heldItem = null;
		heldItemInteraction = null;
	}

	//return true if you can hold something
	public bool TryToPickUp(GameObject g, IHoldable i)
	{
		Debug.Log("trying to pickup a " + g.name);
		if(heldItem == null)
		{
			heldItem = g;
			heldItemInteraction = i;
			return true;
		}
		return false;
	}
}
