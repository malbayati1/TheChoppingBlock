using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RestoreHealth : Effect
{
	// public float currentDuration;
	// public float maxDuration;
	// public float potency

    public override bool OnApply(PlayerEffects p)
	{
		Debug.Log("APPLYING RESTORE HEALTH");
		p.stats.health.SetBaseValue(p.stats.health.value += potency);
		return base.OnApply(p);
	}
    public override void OnRemove(PlayerEffects p)
	{

	}
}
