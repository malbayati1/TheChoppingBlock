using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    public List<GameObject> springSpawnPrefabs;
    public List<GameObject> summerSpawnPrefabs;
    public List<GameObject> fallSpawnPrefabs;
    public List<GameObject> winterSpawnPrefabs;

    void Start()
    {
        UpdateSpawnersAtSeasonStart(Season.Spring);
    }

    private void AddSpawners()
    {
        GameObject go = Instantiate(springSpawnPrefabs[Random.Range(0, springSpawnPrefabs.Count)]);
        go.transform.parent = transform.GetChild(0);

        go = Instantiate(summerSpawnPrefabs[Random.Range(0, summerSpawnPrefabs.Count)]);
        go.transform.parent = transform.GetChild(1);

        go = Instantiate(fallSpawnPrefabs[Random.Range(0, fallSpawnPrefabs.Count)]);
        go.transform.parent = transform.GetChild(2);

        go = Instantiate(winterSpawnPrefabs[Random.Range(0, winterSpawnPrefabs.Count)]);
        go.transform.parent = transform.GetChild(3);
    }

    private void UpdateSpawnersAtSeasonStart(Season s)
    {
        if (s == Season.Spring)
        {
            AddSpawners();
        }
        else if (s == Season.Summer)
        {
            List<SpiderEnemy> spidersToKill = new List<SpiderEnemy>(FindObjectsOfType<SpiderEnemy>());
            for (int i = spidersToKill.Count - 1; i >= 0; i--)
            {
                Destroy(spidersToKill[i].gameObject);
            }
        }
        else if (s == Season.Winter)
        {
            List<SwarmingEnemy> beesToKill = new List<SwarmingEnemy>(FindObjectsOfType<SwarmingEnemy>());
            for (int i = beesToKill.Count - 1; i >= 0; i--)
            {
                Destroy(beesToKill[i].gameObject);
            }
        }


        for (int i = 0; i < 4; i++)
        {
            if (i == (int) s)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        SeasonManager.instance.seasonChangeEvent += UpdateSpawnersAtSeasonStart;
    }

    void OnDisable()
    {
        SeasonManager.instance.seasonChangeEvent -= UpdateSpawnersAtSeasonStart;
    }
}
