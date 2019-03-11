using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookingPot : MonoBehaviour
{
	public GameObject topOfSlotLocation;
	public GameObject cookingUI;

	public float addTime;
	public float dropTime;
	public float spitOutRadius = 4f;

	public delegate void RadiusDelegate();
	public event RadiusDelegate enterRadiusEvent = delegate { };
	public event RadiusDelegate leaveRadiusEvent = delegate { };

	public delegate void MixtureChange();
	public event MixtureChange ingredientAdded = delegate { };
	public event MixtureChange ingredientRemoved = delegate { };

	//our current ingredients
    private Mixture currentMixture;

	private List<GameObject> currentlyInside;
	private List<GameObject> toCheck;
	private List<GameObject> slots;

	private Camera cam;

	void Awake()
	{
		currentMixture = ScriptableObject.CreateInstance("Mixture") as Mixture;
		toCheck = new List<GameObject>();
		currentlyInside = new List<GameObject>();
		cam = Camera.main;
		slots = new List<GameObject>(Mixture.MAXINGREDIENTS);
		foreach(Transform child in transform.GetChild(0))
		{
			slots.Add(child.gameObject);
		}
	}

<<<<<<< HEAD
	public void StartCooking()
	{
		cookingAudioSource.clip = AudioManager.instance.cookAudio;
		cookingAudioSource.loop = true;
		cookingAudioSource.Play();

		StartCoroutine(FailsafeStopCookingSound());
	}

	private IEnumerator FailsafeStopCookingSound()
	{
		yield return new WaitForSeconds(1.5f);

		if (cookingAudioSource.loop)
		{
			StopCooking();
		}
	}

	public void StopCooking()
	{
		cookingAudioSource.loop = false;
		cookingAudioSource.Stop();
	}

=======
>>>>>>> parent of cb7a23a... Merge branch 'master' of https://github.com/malbayati1/TheChoppingBlock
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
		InGameIngredient ingredient = i.GetComponent<InGameIngredient>();
		if(ingredient != null && currentMixture.AddIngredient(ingredient))
        {
			toCheck.Remove(i);
			ingredient.ingredientData.isPreserved = true;
			Debug.Log("Setting preserved " + i.name + " true");

			Vector3 controlPosition = ((i.transform.position + topOfSlotLocation.transform.position) / 2 + topOfSlotLocation.transform.position) / 2;
			controlPosition.Set(controlPosition.x, controlPosition.y + 6f, controlPosition.z);
			StartCoroutine(MoveIngredient(i, addTime, i.transform.position, topOfSlotLocation.transform.position, controlPosition, true, GetEmptySlot()));
        
			Debug.Log("Successfully added " + ingredient.name);
			currentlyInside.Add(i);
        }
        else
        {
            //DropItem(i);
        }
    }

	public GameObject GetEmptySlot()
	{
		for(int x = 0; x < slots.Count; ++x)
		{
			if(slots[x].transform.childCount == 0)
			{
				return slots[x];
			}
		}
		return null;
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
		i.GetComponent<InGameIngredient>().ingredientData.isPreserved = false;
		Debug.Log("Setting preserved " + i.name + " false");
    }

	private IEnumerator MoveIngredient(GameObject i, float moveTime, Vector3 startPosition, Vector3 endPosition, Vector3 controlPosition, bool shrink, GameObject slot = null)
	{		
		Debug.Log(slot);
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
		NavMeshHit hit;
		NavMesh.SamplePosition(new Vector3(i.transform.position.x, 0, i.transform.position.z), out hit, 40f, NavMesh.AllAreas);
		if(slot != null)
		{
			i.transform.SetParent(slot.transform, true);
			i.transform.position = Vector3.zero;
			i.transform.localPosition = Vector3.zero;
			i.transform.localScale = Vector3.one;
		}
		else
		{
			i.transform.SetParent(null);
			i.transform.position = hit.position;
		}
		i.GetComponent<InGameIngredient>().isHeld = false;
	}

    

	void OnTriggerEnter(Collider col)
	{
		PlayerInteraction p;
		InGameIngredient igi;
		GameObject parent = col.gameObject;
		do
		{
			if(igi = parent.GetComponent<InGameIngredient>())
			{
				this.enabled = true;
				if(!toCheck.Contains(parent))
				{
					//Debug.Log("adding toCheck " + parent.name);
					toCheck.Add(parent);
				}
				return;
			}
			if(p = parent.GetComponent<PlayerInteraction>())
			{
				p.useEvent += Cook;
				p.dropEvent += Empty;
<<<<<<< HEAD
				p.startChannelEvent += StartCooking;
				p.stopChannelEvent += StopCooking;
=======
>>>>>>> parent of cb7a23a... Merge branch 'master' of https://github.com/malbayati1/TheChoppingBlock
				//cookingUI.SetActive(true);
				enterRadiusEvent();
				//Debug.Log("enter radius");
				return;
			}
		} while(parent.transform.parent != null && (parent = parent.transform.parent.gameObject));
		
	}

	void OnTriggerExit(Collider col)
    {
		GameObject parent = col.gameObject;
		PlayerInteraction p;
		InGameIngredient igi;
		do
		{
			if(igi = parent.GetComponent<InGameIngredient>())
			{
				//Debug.Log("removing toCheck " + parent.name);
				toCheck.Remove(parent);
				return;
			}
			if(p = parent.GetComponent<PlayerInteraction>())
			{
				p.useEvent -= Cook;
				p.dropEvent -= Empty;
<<<<<<< HEAD
				p.startChannelEvent -= StartCooking;
				p.stopChannelEvent -= StopCooking;
=======
>>>>>>> parent of cb7a23a... Merge branch 'master' of https://github.com/malbayati1/TheChoppingBlock
				//cookingUI.SetActive(false);
				leaveRadiusEvent();
				//Debug.Log("leaving radius");
				return;
			}
		} while(parent.transform.parent != null && (parent = parent.transform.parent.gameObject));
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
	}

	void LateUpdate()
	{
		if(cookingUI.activeInHierarchy)
		{
			cookingUI.transform.position = cam.WorldToScreenPoint(topOfSlotLocation.transform.position);
		}
	}
}
