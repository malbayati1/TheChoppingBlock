using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonUI : MonoBehaviour
{
	private const bool DEBUG = false;

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
		cookingPot.ingredientAdded += UpdateText;
		cookingPot.ingredientRemoved += UpdateText;
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
		if(DEBUG) Debug.Log("updating");
		if(bHoldingIngredient && bInPotRadius)
		{
			if(DEBUG) Debug.Log("1");
			text.text = inPotRadiusHoldingIngredient;
		}
		else if(bHoldingIngredient)
		{
			if(DEBUG) Debug.Log("2");
			text.text = holdingIngredient;
		}
		else if(bHoldingKnife)
		{
			if(DEBUG) Debug.Log("3");
			text.text = holdingKnife;
		}
		else if(bInPotRadius)
		{
			if(cookingPot.IsNotEmpty())
			{
				if(DEBUG) Debug.Log("4");
				text.text = inPotRadius;
			}
			else
			{
				if(DEBUG) Debug.Log("5");
				text.text = "None";
			}
		}
		else
		{
			if(DEBUG) Debug.Log("6");
			text.text = "None";
		}
	}

	void PickUpItem(GameObject g)
	{
		if(DEBUG) Debug.Log("detecting pickup");
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
		if(DEBUG) Debug.Log("detecting drop");
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
		if(DEBUG) Debug.Log("detecting enter");
		bInPotRadius = true;
		UpdateText();
	}

	void LeavePotRadius()
	{
		if(DEBUG) Debug.Log("detecting exit");
		bInPotRadius = false;
		UpdateText();
	}
}