using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPatch : MonoBehaviour
{
	public List<GameObject> grassPrefabs;

	public float minRadius;
	public float maxRadius;
	public int numberOfGrass;
    
    void Start()
    {
		for(int x = 0; x < numberOfGrass; ++x)
		{
			/*
			Vector3 rand = Random.insideUnitSphere;
			rand.Set(rand.x, 0, rand.z);
			rand.Normalize();
			rand *= Random.Range(minRadius, maxRadius);
			GameObject temp = Instantiate(grassPrefabs[Random.Range(0, grassPrefabs.Count)], transform.position + rand, Quaternion.AngleAxis(Random.Range(0f, 360), Vector3.up), transform);
			*/
			//want it to bunch up, and make small patches, maybe make it more consistent
		}
    }
}
