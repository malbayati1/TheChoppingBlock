using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
	//our current ingredients
    public Mixture currentMixture;

	private List<GameObject> currentlyInside;
	private List<GameObject> toCheck;

	void Awake()
	{
		currentMixture = ScriptableObject.CreateInstance("Mixture") as Mixture;
		toCheck = new List<GameObject>();
		currentlyInside = new List<GameObject>();
		this.enabled = false;
	}

	//call when you want the pot to combine ingredient
    public void Cook()
    {
		if(currentMixture.ingredients.Count == 0) //can't cook with nothing inside
		{
			return;
		}
		Debug.Log("trying to cook");
		GameObject spawn = Instantiate(RecipeManager.instance.GetResult(currentMixture), transform.position + Vector3.up * 2, Quaternion.identity);
		currentMixture = ScriptableObject.CreateInstance("Mixture") as Mixture;
		foreach(GameObject g in currentlyInside)
		{
			Destroy(g);
		}
		currentlyInside = new List<GameObject>();
    }

    public void Add(GameObject i)
    {
		toCheck.Remove(i);
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

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Ingredient"))
		{
			this.enabled = true;
			GameObject parent = col.gameObject;
			while(parent.transform.parent != null)
			{
				parent = parent.transform.parent.gameObject;
			}
			if(!toCheck.Contains(parent))
			{
				toCheck.Add(parent);
			}
		}
		if(col.gameObject.CompareTag("Player"))
		{
			col.gameObject.GetComponent<PlayerInteraction>().useEvent += Cook;
		}
	}

	void OnTriggerExit(Collider col)
    {
		if(col.gameObject.CompareTag("Ingredient"))
		{
			GameObject parent = col.gameObject;
			while(parent.transform.parent != null)
			{
				parent = parent.transform.parent.gameObject;
			}
			toCheck.Remove(parent);
		}
		if(col.gameObject.CompareTag("Player"))
		{
			col.gameObject.GetComponent<PlayerInteraction>().useEvent -= Cook;
		}
	}

	//keeps a running track of items inside of it to make sure that they don't become legal
	//for example if the player enters while holding it, then drops it
	void Update()
	{
		for(int x = toCheck.Count - 1; x >= 0; --x)
		{
			if(toCheck == null)
			{
				toCheck.RemoveAt(x);
			}
			else if(!toCheck[x].GetComponent<InGameIngredient>().isHeld)
			{
				Add(toCheck[x].gameObject);
			}
		}
		if(toCheck.Count <= 0)
		{
			this.enabled = false;
		}
	}
}
