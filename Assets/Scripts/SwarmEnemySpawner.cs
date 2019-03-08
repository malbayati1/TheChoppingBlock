using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float timeBetweenSpawns;

    public int numSpawnsAllowedActive;

	[SerializeField]
    public Vector3[] spawnLocations;

    private GameObject[] activeEnemies;

    private float timer;

    void Awake()
    {
		activeEnemies = new GameObject[spawnLocations.Length];
		timer = 0;
    }

    void Update()
    {
		timer += Time.deltaTime;
		if(timer >= timeBetweenSpawns)
		{
			timer = 0;
			SpawnNewEnemy();
		}
	}

	void SpawnNewEnemy()
	{
		List<int> emptyIndices = FindEmptyIndices();
		if(emptyIndices.Count == 0 || emptyIndices.Count <= spawnLocations.Length - numSpawnsAllowedActive)
		{
			return;
		}
		int index = emptyIndices[Random.Range(0, emptyIndices.Count)];
		NavMeshHit hit;
		if(NavMesh.SamplePosition(new Vector3(Random.Range(-140f, 140f), 0, Random.Range(-140f, 140f)), out hit, 1.0f, NavMesh.AllAreas))
		{ 
			activeEnemies[index] = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
		}
		else
		{
			NavMesh.SamplePosition(new Vector3(Random.Range(-140f, 140f), 0, Random.Range(-140f, 140f)), out hit, 300.0f, NavMesh.AllAreas);
			activeEnemies[index] = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
		}
		
	}

	List<int> FindEmptyIndices()
	{
		List<int> temp = new List<int>();
		for(int x = 0; x < activeEnemies.Length; ++x)
		{
			if(activeEnemies[x] == null)
			{
				temp.Add(x);
			}
		}
		return temp;
	}

}
