using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour, IHoldable
{
	private const float PICKUPCOOLDOWN = 3f;

	public bool isHeld;

	private bool canBePickedUp;

	private BoxCollider2D collider2D;

	protected virtual void Awake()
	{
		collider2D = GetComponent<BoxCollider2D>();
	}

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
		//collider2D.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(canBePickedUp && col.gameObject.CompareTag("Player"))
		{
			if(col.gameObject.GetComponent<PlayerInteraction>().TryToPickUp(gameObject, this))
			{
				//collider2D.enabled = false;
				canBePickedUp = false;
				isHeld = true;
			}			
		}
	}
}
