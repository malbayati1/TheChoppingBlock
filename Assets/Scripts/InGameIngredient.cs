﻿using System.Collections;
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
		if(ingredientData.effects.Count != ingredientData.effectData.Count)
		{
			Debug.Log("ERROR SPAWNING " + gameObject.name);
			Destroy(gameObject);
			return;
		}
		for(int x = ingredientData.effects.Count - 1; x >= 0; --x)
		{
			ingredientData.effects[x] = Object.Instantiate(ingredientData.effects[x]) as Effect;
			ingredientData.effects[x].potency = ingredientData.effectData[x].potency;
			ingredientData.effects[x].maxDuration = ingredientData.effectData[x].duration;
		}
	}

	void Update()
	{
		if(isHeld)
		{
			ingredientData.isPreserved = true;
			return;
		}
		ingredientData.isPreserved = false;
		transform.RotateAround(transform.position, Vector3.up, rotationDegreesPerSecond * Time.deltaTime);
	}

    public override bool Use(GameObject user)
	{
		PlayerEffects pe = user.GetComponent<PlayerEffects>();
		StartCoroutine(AddEffects(pe));
		return true;
	}

	IEnumerator AddEffects(PlayerEffects pe)
	{
		foreach(Effect e in ingredientData.effects)
		{
			pe.AddEffect(e);
			yield return new WaitForSeconds(0.2f);
		}
		Destroy(gameObject);
	}

    public override void Drop(GameObject from)
	{
		base.Drop(from);
	}
}
