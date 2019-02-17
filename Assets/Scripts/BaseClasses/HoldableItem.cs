using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour, IHoldable
{
	private const float PICKUPCOOLDOWN = 3f;

	public bool isHeld;

	private bool canBePickedUp;

	protected virtual void Start()
	{
		canBePickedUp = true;
	}

	public virtual void Use(GameObject user)
	{

	}

	public virtual void Drop(GameObject droppedBy)
	{
		StartCoroutine(PickupCooldown());
	}

	IEnumerator PickupCooldown()
	{
		isHeld = false;
		yield return new WaitForSeconds(PICKUPCOOLDOWN);
		canBePickedUp = true;
	}

	void OnTriggerEnter(Collider col)
    {
		//Debug.Log("entering");
		if(canBePickedUp && col.gameObject.CompareTag("Player"))
		{
			if(col.gameObject.GetComponent<PlayerInteraction>().TryToPickUp(gameObject, this))
			{
				canBePickedUp = false;
				isHeld = true;
			}			
		}
	}
}
