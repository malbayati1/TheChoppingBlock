using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
	public GameObject topOfSlotLocation;

	public float addTime;
	public float dropTime;
	public float spitOutRadius = 4f;

	//our current ingredients
    [HideInInspector] public Mixture currentMixture;

	private List<GameObject> currentlyInside;
	[SerializeField]private List<GameObject> toCheck;

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
		GameObject spawn = RecipeManager.instance.GetResult(currentMixture);
		spawn.transform.position = transform.position + Vector3.up * 2;
		spawn.GetComponent<InGameIngredient>().isHeld = true;
		DropItem(spawn);
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
		InGameIngredient ingredient = i.GetComponent<InGameIngredient>();

		Vector3 controlPosition = ((i.transform.position + topOfSlotLocation.transform.position) / 2 + topOfSlotLocation.transform.position) / 2;
		controlPosition.Set(controlPosition.x, controlPosition.y + 6f, controlPosition.z);
        StartCoroutine(MoveIngredient(i, addTime, i.transform.position, topOfSlotLocation.transform.position, controlPosition, true));
        
        if(ingredient != null && currentMixture.AddIngredient(ingredient))
        {
			Debug.Log("Successfully added " + ingredient.name);
			currentlyInside.Add(i);
        }
        else
        {
            DropItem(i);
        }
    }

	public void Empty()
    {
        for(int x = currentlyInside.Count - 1; x >= 0; --x)
        {
            DropItem(currentlyInside[x]);
        }
        currentMixture.ingredients = new List<Ingredient>();
		currentlyInside = new List<GameObject>();
    }

	public void DropItem(GameObject i)
    {
		toCheck.Remove(i);
		Vector3 end = Random.insideUnitSphere;
		end.Set(end.x, 0, end.z);
		end.Normalize();
		end *= spitOutRadius;
		end += transform.position;
		Vector3 controlPosition = (transform.position + end) / 2;
		controlPosition.Set(controlPosition.x, controlPosition.y + 6f, controlPosition.z);
        StartCoroutine(MoveIngredient(i, dropTime ,transform.position, end, controlPosition, false));
    }

	private IEnumerator MoveIngredient(GameObject i, float moveTime, Vector3 startPosition, Vector3 endPosition, Vector3 controlPosition, bool shrink)
	{		
		i.GetComponent<InGameIngredient>().isHeld = true;
		float timer = 0;
		float t;
		while(timer < moveTime)
		{
			timer += Time.deltaTime;
			t = timer / moveTime;
			//B E Z I E R C U R V E S don't ask how this works I don't know
			i.transform.position = (1 - t) * (1 - t) * startPosition + 2 * (1 - t) * t * controlPosition + t * t * endPosition;
			//shrink the scale if bool is set, otherwise just set it as 1
			i.transform.localScale = Vector3.one * ((shrink) ?  1 - t : 1);
			yield return null;
		}
		i.GetComponent<InGameIngredient>().isHeld = false;
	}

    

	void OnTriggerEnter(Collider col)
	{
		GameObject parent = col.gameObject;
		while(parent.transform.parent != null)
		{
			parent = parent.transform.parent.gameObject;
		}
		if(parent.CompareTag("Ingredient"))
		{
			//Debug.Log("adding toCheck " + parent.name);
			this.enabled = true;
			if(!toCheck.Contains(parent))
			{
				//Debug.Log("adding toCheck " + parent.name);
				toCheck.Add(parent);
			}
		}
		if(parent.CompareTag("Player"))
		{
			PlayerInteraction p = parent.GetComponent<PlayerInteraction>();
			p.useEvent += Cook;
			p.dropEvent += Empty;
		}
	}

	void OnTriggerExit(Collider col)
    {
		GameObject parent = col.gameObject;
		while(parent.transform.parent != null)
		{
			parent = parent.transform.parent.gameObject;
		}
		if(parent.CompareTag("Ingredient"))
		{
			//Debug.Log("removing toCheck " + parent.name);
			toCheck.Remove(parent);
		}
		if(parent.CompareTag("Player"))
		{
			PlayerInteraction p = parent.GetComponent<PlayerInteraction>();
			p.useEvent -= Cook;
			p.dropEvent -= Empty;
		}
	}

	//keeps a running track of items inside of it to make sure that they don't become legal
	//for example if the player enters while holding it, then drops it
	void Update()
	{
		for(int x = toCheck.Count - 1; x >= 0; --x)
		{
			//Debug.Log("checking " + toCheck[x].name);
			if(toCheck[x] == null)
			{
				toCheck.RemoveAt(x);
			}
			else if(!toCheck[x].GetComponent<InGameIngredient>().isHeld && !currentlyInside.Contains(toCheck[x]))
			{
				Add(toCheck[x]);
			}
		}
		if(toCheck.Count <= 0)
		{
			this.enabled = false;
		}
	}
}
