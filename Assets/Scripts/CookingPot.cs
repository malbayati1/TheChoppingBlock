using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
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

    public void Cook()
    {

    }

    public void Add(GameObject i)
    {
        i.transform.position = Vector2.one * 9999;
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
		i.transform.position = transform.position + Vector3.right * 3;
        //should drop the item in such a way as it flies away from the pot and doesn't get readded
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
