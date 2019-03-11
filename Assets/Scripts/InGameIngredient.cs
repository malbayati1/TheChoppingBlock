using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the actual object in the scene of the ingredient

public class InGameIngredient : HoldableItem
{
	public Ingredient ingredientData; //holds an asset of an ingredient

	private float rotationDegreesPerSecond = 180f;

	private AudioSource audioSource;

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
		if(!gameObject.CompareTag("Ingredient"))
		{
			Debug.LogError("THIS INGREDIENT(" +gameObject.name+") IS NOT TAGGED CORRECTLY");
		}

		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.spatialBlend = 1;
	}

	void Update()
	{
		if(isHeld)
		{
			return;
		}
		transform.RotateAround(transform.position, Vector3.up, rotationDegreesPerSecond * Time.deltaTime);
	}

	protected override void GetPickedUp(Collider col)
	{
		base.GetPickedUp(col);

		audioSource.clip = AudioManager.instance.pickUpIngredientAudio;
        audioSource.Play();
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

		audioSource.clip = AudioManager.instance.dropIngredientAudio;
        audioSource.Play();
	}
}
