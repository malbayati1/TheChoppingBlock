using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Repel : Effect
{
    // public float currentDuration;
	// public float maxDuration;
	// public float potency

    public override bool OnApply(PlayerEffects p)
	{
		Debug.Log("APPLYING REPEL");
		p.GetComponent<NavMeshAgent>().radius += 5f;
		return base.OnApply(p);
	}
    public override void OnRemove(PlayerEffects p)
	{
		Debug.Log("REMOVING REPEL");
		p.GetComponent<NavMeshAgent>().radius -= 5f;
	}
}
