using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the actual object in the scene of the ingredient

public class Weapon : HoldableItem
{
	public Vector3 heldPositionOffset = new Vector3(0f, 0f, -.5f);
	public Vector3 heldRotationOffset = new Vector3(-90f, 180f, 0f);

	public float knockbackModifier = 1f;

	public int damageModifier = 1;
	
	private float rotationDegreesPerSecond = 180f;

	private Transform modelChild;

	void Awake()
	{
		modelChild = transform.GetChild(0);
	}

	void Update()
	{
		if(isHeld)
		{
			modelChild.transform.localPosition = heldPositionOffset;
			Quaternion rotationOffset = new Quaternion();
			rotationOffset.eulerAngles = heldRotationOffset;
			modelChild.transform.localRotation = rotationOffset;
			return;
		}
		modelChild.transform.localPosition = new Vector3();
		modelChild.transform.localRotation = new Quaternion();
		transform.RotateAround(transform.position, Vector3.up, rotationDegreesPerSecond * Time.deltaTime);
	}

    public override bool Use(GameObject user)
	{
		return false;
	}

    public override void Drop(GameObject from)
	{
		base.Drop(from);
	}

	protected override void OnTriggerEnter(Collider col)
    {
		base.OnTriggerEnter(col);

		if (isHeld  && col.gameObject.CompareTag("Enemy"))
		{
			Unit unit = col.gameObject.GetComponent<Unit>();
			if (unit != null)
			{
				PlayerStats stats = heldBy.GetComponent<PlayerStats>();
				int damage = Mathf.RoundToInt(stats.strength.value) * damageModifier;
				float knockback = knockbackModifier * stats.strength.value;
				Vector3 direction = transform.forward.normalized;

				unit.GetHit(damage, direction, knockback);
			}
		}
	}
}
