using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonUI : MonoBehaviour
{
	public string holdingIngredient;
	public string inPotRadius;
	public string inPotRadiusHoldingIngredient;
	public string holdingKnife;

	private Text text;
	private PlayerInteraction playerInteraction;
	private CookingPot cookingPot;
	
	private bool bHoldingIngredient;
	private bool bInPotRadius;
	private bool bHoldingKnife;

	void Start()
	{
		text = transform.GetChild(2).gameObject.GetComponent<Text>();
		UpdateText();
	}

    void OnEnable()
	{
		playerInteraction = GameObject.FindWithTag("Player").GetComponent<PlayerInteraction>();
		playerInteraction.ingredientEatEvent += DropItem;
		playerInteraction.itemDropEvent += DropItem;
		playerInteraction.itemPickupEvent += PickUpItem;
		cookingPot = GameObject.FindWithTag("CookingPot").GetComponent<CookingPot>();
		cookingPot.enterRadiusEvent += EnterPotRadius;
		cookingPot.leaveRadiusEvent += LeavePotRadius;
	}

	void OnDisable()
	{
		playerInteraction.ingredientEatEvent -= DropItem;
		playerInteraction.itemDropEvent -= DropItem;
		playerInteraction.itemPickupEvent -= PickUpItem;
		cookingPot.enterRadiusEvent -= EnterPotRadius;
		cookingPot.leaveRadiusEvent -= LeavePotRadius;
	}

	void UpdateText()
	{
		if(bHoldingIngredient && bInPotRadius)
		{
			text.text = inPotRadiusHoldingIngredient;
		}
		else if(bHoldingIngredient)
		{
			text.text = holdingIngredient;
		}
		else if(bHoldingKnife)
		{
			text.text = holdingKnife;
		}
		else if(bInPotRadius)
		{
			text.text = inPotRadius;
		}
		else
		{
			text.text = "None";
		}
	}

	void PickUpItem(GameObject g)
	{
		if(g.GetComponent<Weapon>() == null)
		{
			bHoldingIngredient = true;
		}
		else
		{
			bHoldingKnife = true;
		}
		UpdateText();
	}

	void DropItem(GameObject g)
	{
		if(g.GetComponent<Weapon>() == null)
		{
			bHoldingIngredient = false;
		}
		else
		{
			bHoldingKnife = false;
		}
		UpdateText();
	}

	void EnterPotRadius()
	{
		bInPotRadius = true;
		UpdateText();
	}

	void LeavePotRadius()
	{
		bInPotRadius = false;
		UpdateText();
	}
}
