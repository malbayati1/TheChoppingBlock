using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType enemyType;

    public float timeBetweenSpawns;

    public int numSpawnsAllowedActive;

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

        if (timeSinceLastSpawn > timeBetweenSpawns && activeEnemies.Count < numSpawnsAllowedActive)
        {
            GameObject newEnemy = EnemySpawnManager.instance.SpawnEnemy(enemyType);
            if (newEnemy != null)
            {
                activeEnemies.Add(newEnemy);
                timeSinceLastSpawn = 0f;
            }
        }

        //If statement to prevent enemies from spawning the moment one of them dies
        if (activeEnemies.Count < numSpawnsAllowedActive)
            timeSinceLastSpawn += Time.deltaTime;
    }
}
