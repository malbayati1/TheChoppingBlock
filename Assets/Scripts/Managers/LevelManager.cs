using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum Rarity { Common, Uncommon, Rare };

public class LevelManager : Singleton<LevelManager>
{
	private GameObject navMesh;
    public List<GameObject> summerIngredients;
    public List<GameObject> springIngredients;
    public List<GameObject> fallIngredients;
    public List<GameObject> winterIngredients;
    private List<GameObject> seasonalIngredients;   // MA 2/25: This will be used later to determine which season it is
    private GameObject floor;

    public float[] rarityTable = new float[Enum.GetNames(typeof(Rarity)).Length];

    [SerializeField]private List<GameObject> spawnedObjects;
	private LayerMask terrain;

    void Start()
    {
        floor = GameObject.Find("NavMesh");
        spawnedObjects = new List<GameObject>();
		terrain = 1 << LayerMask.NameToLayer("Terrain");
        SpawnIngredients(SeasonManager.instance.GetCurrentSeason());
    }

    void OnEnable()
    {
        SeasonManager.instance.seasonChangeEvent += HandleSeasonChange;
    }

    void OnDisable()
    {
        SeasonManager.instance.seasonChangeEvent -= HandleSeasonChange;
    }

    void HandleSeasonChange(Season s)
    {
        DespawnIngredients();
        SpawnIngredients(s);
    }

	//called when the seasons change
    void DespawnIngredients()
    {
		InGameIngredient ing;
        for(int x = spawnedObjects.Count - 1; x >= 0; x--)
        {
            if(spawnedObjects[x] != null)
            {
				ing = spawnedObjects[x].GetComponent<InGameIngredient>();
				if(!ing.isHeld && !ing.ingredientData.isPreserved)
				{
					// Debug.Log("killing " + spawnedObjects[x].name);
                	Destroy(spawnedObjects[x]); // MA 2/26/19: The if statement is causing an exception
                                            // Will resolve laters
				}
            }
			else
			{
				spawnedObjects.RemoveAt(x);
			}
        }
    }
    
    void SpawnIngredients(Season s)
    {
        // MA 2/25: Assign the seasonalIngredients to one of the other lists of ingredients
        switch(s)
        {
            case Season.Summer:
                seasonalIngredients = summerIngredients;
                break;
            case Season.Fall:
                seasonalIngredients = fallIngredients;
                break;
            case Season.Winter:
                seasonalIngredients = winterIngredients;
                break;
            case Season.Spring:
                seasonalIngredients = springIngredients;
                break;
            default:
                seasonalIngredients = summerIngredients;
                break;
        }
        // MA 2/25: Go through the list of ingredients and spawn the ingredients in random places
        for(int yikes = 0; yikes < 10; ++yikes)
        for (int i = 0; i < seasonalIngredients.Count; i++)
        {
            NavMeshHit hit;
			Vector3 temp = new Vector3(Random.Range(-55, 55f), 0, Random.Range(-55f, 55f));
			NavMesh.SamplePosition(temp, out hit, 20.0f, NavMesh.AllAreas);
			//Debug.DrawLine(hit.position + Vector3.down * 50, hit.position + Vector3.up * 100, Color.red, 200);
			//RaycastHit t = new RaycastHit();
			if(!hit.hit || Physics.Linecast(hit.position + Vector3.down * 50, hit.position + Vector3.up * 50, terrain))
			{
				//Debug.Log("failed at " + t.collider.gameObject);
				continue;
			}
            
            Vector3 spawnPosition = hit.position;   // Position to the nearest point on the mesh of the specified point
            if (spawnPosition.magnitude < 10)    // if spawning in position of pot, readjust x and z
            {
                if(spawnPosition.x < 0)
                {
                    spawnPosition.x -= 8.5f;
                }
                else
                {
                    spawnPosition.x += 8.5f;
                }
                if(spawnPosition.z < 0)
                {
                    spawnPosition.z -= 8.5f;
                }
                else
                {
                    spawnPosition.z += 8.5f;
                }
            }
            
            spawnedObjects.Add(Instantiate(seasonalIngredients[i], spawnPosition, Quaternion.identity));

        }
    }
}
