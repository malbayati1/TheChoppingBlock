using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Rarity { Common, Uncommon, Rare };

public class LevelManager : Singleton<LevelManager>
{
    public List<GameObject> summerIngredients;
    public List<GameObject> springIngredients;
    public List<GameObject> fallIngredients;
    public List<GameObject> winterIngredients;
    private List<GameObject> seasonalIngredients;   // MA 2/25: This will be used later to determine which season it is

    public float[] rarityTable = new float[Enum.GetNames(typeof(Rarity)).Length];

    private List<GameObject> spawnedObjects;

    void Start()
    {
        spawnedObjects = new List<GameObject>();
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
        for(int x = spawnedObjects.Count - 1; x >= 0; x--)
        {
            //if(!spawnedObjects[x].GetComponent<Ingredient>().isPreserved)
            //{
                Destroy(spawnedObjects[x]); // MA 2/26/19: The if statement is causing an exception
                                            // Will resolve laters
            //}
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
        for(int i = 0; i < seasonalIngredients.Count; i++)
        {
            spawnedObjects.Add(Instantiate(seasonalIngredients[i], new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f)), Quaternion.identity));

        }
    }
}
