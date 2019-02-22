using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the actual object in the scene of the ingredient

public class Weapon : HoldableItem
{
	public Vector3 heldPositionOffset = new Vector3(0f, 0f, -.5f);
	public Vector3 heldRotationOffset = new Vector3(-90f, 180f, 0f);
	
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

    public override void Use(GameObject user)
	{
		
	}

    public override void Drop(GameObject from)
	{
		base.Drop(from);
	}
}
