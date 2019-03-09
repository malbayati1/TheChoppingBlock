using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowEnemies : Effect
{
	// public float currentDuration;
	// public float maxDuration;
	// public float potency

	private Dictionary<NavMeshAgent, float> affected;

	public override bool OnApply(PlayerEffects p)
	{
		affected = new Dictionary<NavMeshAgent, float>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		NavMeshAgent nma;
		foreach(GameObject g in enemies)
		{
			nma = g.GetComponent<NavMeshAgent>();
			if(nma != null)
			{
				affected[nma] = nma.speed;
				nma.speed /=  (1 + potency);
			}
		}
		return base.OnApply(p);
	}

	public override void OnRemove(PlayerEffects p)
	{
		foreach(KeyValuePair<NavMeshAgent, float> item in affected)
		{
			item.Key.speed = item.Value;
		}
	}
}
