using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : Effect
{
	// public float currentDuration;
	// public float maxDuration;
	// public float potency

    public override bool OnApply(PlayerEffects p)
	{
		//Debug.Log("APPLYING RESTORE HEALTH");
		p.stats.health.Heal(potency);
		return base.OnApply(p);
	}
}
