using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the actual object in the scene of the ingredient

public class InGameIngredient : HoldableItem
{
	public Ingredient ingredientData; //holds an asset of an ingredient

	void Awake()
	{
		ingredientData = Object.Instantiate(ingredientData) as Ingredient;
		ingredientData.effect = Object.Instantiate(ingredientData.effect) as Effect;
		ingredientData.effect.maxDuration = ingredientData.duration;
		ingredientData.effect.currentDuration = ingredientData.duration;
	}

    public override void Use(GameObject user)
	{
		user.GetComponent<PlayerEffects>().AddEffect(ingredientData.effect);
		Destroy(gameObject);
	}

    public override void Drop(GameObject from)
	{
		base.Drop(from);
	}
}
