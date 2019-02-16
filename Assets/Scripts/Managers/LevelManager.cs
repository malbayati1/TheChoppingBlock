using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity { Common, Uncommon, Rare };

public class LevelManager : Singleton<LevelManager>
{
    public List<GameObject> summerIngredients;
    public List<GameObject> springIngredients;
    public List<GameObject> fallIngredients;
    public List<GameObject> winterIngredients;

    public float[] rarityTable = new float[Enum.GetNames(typeof(Rarity)).Length];

    private List<GameObject> spawnedObjects;

    void Start()
    {
        spawnedObjects = new List<GameObject>();
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

    void DespawnIngredients()
    {
        for(int x = spawnedObjects.Count - 1; x >= 0; x--)
        {
            if(!spawnedObjects[x].GetComponent<Ingredient>().isPreserved)
            {
                Destroy(spawnedObjects[x]);
            }
        }
    }
    
    void SpawnIngredients(Season s)
    {
        //spawn ingredients based on the season, and the rarit table
        //probably need to determine where to spawn the objects
    }
}
