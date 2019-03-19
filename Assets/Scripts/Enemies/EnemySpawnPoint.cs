using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnPoint : MonoBehaviour
{


    public EnemyType enemyType;

    public float radius = 5f;

    public GameObject SpawnEnemy(GameObject enemyToSpawnPrefab)
    {
        Vector2 offset = (Random.insideUnitCircle * radius);
        Vector3 location = transform.position + new Vector3(offset.x, 0, offset.y);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(location, out hit, 5f, NavMesh.AllAreas))
        {
            Debug.Log(hit.position);
            location = hit.position;
        }
        else
        {
            return null;
        }
        if (Vector3.Distance(location, GameObject.FindWithTag("Player").transform.position) < 5f)
        {
            return null; //Don't keep trying in case nothing is close
        }
        GameObject newEnemy = GameObject.Instantiate(enemyToSpawnPrefab);
        newEnemy.SetActive(false);
        newEnemy.transform.parent = transform;
        newEnemy.transform.position = location;
        newEnemy.SetActive(true);

        return newEnemy;
    }
}
