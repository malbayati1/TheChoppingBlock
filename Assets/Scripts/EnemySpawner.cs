using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawnPrefab;

    public float timeBetweenSpawns;

    public int numSpawnsAllowedActive;

    [SerializeField]
    public List<Vector3> spawnLocations;

    private List<GameObject> activeEnemies;

    private float timeSinceLastSpawn = 0f;

    void Awake()
    {
        activeEnemies = new List<GameObject>();
    }

    void Update()
    {
        List<GameObject> livingSpawns = new List<GameObject>();
        foreach (GameObject go in activeEnemies)
        {
            if (go)
            {
                livingSpawns.Add(go);
            }
        }

        activeEnemies = livingSpawns;

        if (timeSinceLastSpawn > timeBetweenSpawns && activeEnemies.Count < numSpawnsAllowedActive && spawnLocations.Count > 0)
        {
            Vector3 location = spawnLocations[Random.Range(0, spawnLocations.Count)];
            if (Vector3.Distance(location, GameObject.FindWithTag("Player").transform.position) < 5f)
            {
                return; //Don't keep trying in case nothing is close
            }
            GameObject newEnemy = GameObject.Instantiate(enemyToSpawnPrefab);
            newEnemy.transform.position = location;
            activeEnemies.Add(newEnemy);
            timeSinceLastSpawn = 0f;
        }

        //If statement to prevent enemies from spawning the moment one of them dies
        if (activeEnemies.Count < numSpawnsAllowedActive)
            timeSinceLastSpawn += Time.deltaTime;
    }
}
