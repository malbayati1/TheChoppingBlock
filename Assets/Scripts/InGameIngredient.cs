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

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			Debug.Log("collide with player");
			col.gameObject.GetComponent<PlayerEffects>().AddEffect(ingredientData.effect);
		}
	}
}
