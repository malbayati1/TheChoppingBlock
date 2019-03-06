using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IngredientSpawner : MonoBehaviour
{
	public List<GameObject> toSpawn;
	public Vector3 startingPositionOffset;
	public Vector3 endPositionOffset;

	private List<GameObject> spawned;

	void Awake()
	{
		spawned = new List<GameObject>();
		SpawnIngredients();
	}

	void Update()
	{
		for(int x = 0; x < spawned.Count; ++x)
		{
			if(spawned[x] == null)
			{
				DespawnIngredients();
				SpawnIngredients();
			}
		}
	}

	void DespawnIngredients()
	{
		for(int x = 0; x < spawned.Count; ++x)
		{
			Destroy(spawned[x]);
		}
		spawned = new List<GameObject>();
	}

	void SpawnIngredients()
	{
		Vector3 step = ((endPositionOffset + transform.position) - (startingPositionOffset + transform.position)) / toSpawn.Count;
		for(int x = 0; x < toSpawn.Count; ++x)
		{
			spawned.Add(Instantiate(toSpawn[x], transform.position + startingPositionOffset + step * x, Quaternion.identity));
		}
	}
}
