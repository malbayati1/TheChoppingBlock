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
		Debug.Log("updating");
		if(bHoldingIngredient && bInPotRadius)
		{
			Debug.Log("1");
			text.text = inPotRadiusHoldingIngredient;
		}
		else if(bHoldingIngredient)
		{
			Debug.Log("2");
			text.text = holdingIngredient;
		}
		else if(bHoldingKnife)
		{
			Debug.Log("3");
			text.text = holdingKnife;
		}
		else if(bInPotRadius)
		{
			if(cookingPot.IsNotEmpty())
			{
				Debug.Log("4");
				text.text = inPotRadius;
			}
			else
			{
				Debug.Log("5");
				text.text = "None";
			}
		}
		else
		{
			Debug.Log("6");
			text.text = "None";
		}
	}

	void PickUpItem(GameObject g)
	{
		Debug.Log("detecting pickup");
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
		Debug.Log("detecting drop");
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
		Debug.Log("detecting enter");
		bInPotRadius = true;
		UpdateText();
	}

	void LeavePotRadius()
	{
		Debug.Log("detecting exit");
		bInPotRadius = false;
		UpdateText();
	}
}
