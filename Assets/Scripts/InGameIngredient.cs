using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameIngredient : MonoBehaviour, IHoldable
{
	public Ingredient ingredientData;

	private SpriteRenderer sprite;

	void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	void Start()
	{

	}

    public void Use()
	{

	}

    public void Drop()
	{

	}
}
