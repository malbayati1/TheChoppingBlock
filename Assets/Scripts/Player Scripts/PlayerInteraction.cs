using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public delegate void InputDelegate();
    public event InputDelegate useEvent = delegate {};
	public event InputDelegate dropEvent = delegate {};

	private GameObject heldItem;
	private HoldableItem heldItemInteraction;

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
			return;
		}
		
		if(Input.GetButtonDown("Use"))
		{
			useEvent();
		}
		if(Input.GetButtonDown("Drop"))
		{
			dropEvent();
		}
    }

	//will need to be updated
	//Probably want the item to be offset from the player in the direction they are facing
	void UpdateHoldablePosition()
	{
		heldItemInteraction.gameObject.transform.position = transform.position + CameraController.instance.forwardDirection.normalized;
	}

	//Called when the item is used or dropped
	void ClearFields()
	{
		heldItem = null;
		heldItemInteraction = null;
	}

	//return true if you can hold something
	public bool TryToPickUp(GameObject g, HoldableItem i)
	{
		//Debug.Log("trying to pickup a " + g.name);
		if(heldItem == null)
		{
			heldItem = g;
			heldItemInteraction = i;
			return true;
		}
		return false;
	}
}
