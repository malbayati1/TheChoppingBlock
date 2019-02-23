using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the actual object in the scene of the ingredient

public class InGameIngredient : HoldableItem
{
	public Ingredient ingredientData; //holds an asset of an ingredient

	private float rotationDegreesPerSecond = 180f;

	void Awake()
	{
		ingredientData = Object.Instantiate(ingredientData) as Ingredient;
		ingredientData.effect = Object.Instantiate(ingredientData.effect) as Effect;
		ingredientData.effect.maxDuration = ingredientData.duration;
		ingredientData.effect.currentDuration = ingredientData.duration;
		ingredientData.effect.potency = ingredientData.potency;
	}

	void Update()
	{
		if(isHeld)
		{
			return;
		}
		transform.RotateAround(transform.position, Vector3.up, rotationDegreesPerSecond * Time.deltaTime);
	}

    public override bool Use(GameObject user)
	{
		user.GetComponent<PlayerEffects>().AddEffect(ingredientData.effect);
		Destroy(gameObject);
		return true;
	}

    public override void Drop(GameObject from)
	{
		base.Drop(from);
	}
}
