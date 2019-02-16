using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
	//our current ingredients
    public Mixture currentMixture;

	private List<GameObject> currentlyInside;
	private List<InGameIngredient> toCheck;

	void Awake()
	{
		currentMixture = ScriptableObject.CreateInstance("Mixture") as Mixture;
		toCheck = new List<InGameIngredient>();
		currentlyInside = new List<GameObject>();
		this.enabled = false;
	}

	//call when you want the pot to combine ingredient
    public void Cook()
    {

    }

    public void Add(GameObject i)
    {
        i.transform.position = Vector2.one * 9999; //brings ingredients out of the way while they're inside
        InGameIngredient ingredient = i.GetComponent<InGameIngredient>();
        if(ingredient != null && currentMixture.AddIngredient(ingredient))
        {
			Debug.Log("Successfully added " + ingredient.name);
			currentlyInside.Add(i);
            //nothing?
        }
        else
        {
            DropItem(i);
        }
    }

    public void DropItem(GameObject i)
    {
		i.transform.position = transform.position + Vector3.right * 3; //temp
        //should drop the item in such a way as it flies away from the pot and doesn't get readded
		//needs a coroutine probably to make it look nice
    }

    public void Empty()
    {
        foreach(GameObject g in currentlyInside)
        {
            DropItem(g);
        }
        currentMixture.ingredients = new List<Ingredient>();
		currentlyInside = new List<GameObject>();
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Ingredient"))
		{
			this.enabled = true;
			toCheck.Add(col.gameObject.GetComponent<InGameIngredient>());
		}
	}

	void OnTriggerExit2D(Collider2D col)
    {
		if(col.gameObject.CompareTag("Ingredient"))
		{
			toCheck.Remove(col.gameObject.GetComponent<InGameIngredient>());
		}
	}

	//keeps a running track of items inside of it to make sure that they don't become legal
	//for example if the player enters while holding it, then drops it
	void Update()
	{
		foreach(InGameIngredient i in toCheck)
		{
			if(!i.isHeld)
			{
				Add(i.gameObject);
			}
		}
		if(toCheck.Count <= 0)
		{
			this.enabled = false;
		}
	}
}
