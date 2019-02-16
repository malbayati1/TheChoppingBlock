using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableAssets/Effect", order = 1)]
public class RestoreHealth : Effect
{
	// public float currentDuration;
	// public float maxDuration;
	// public float potency

    public override bool OnApply(PlayerEffects p)
	{
		p.stats.health.SetBaseValue(p.stats.health.value += potency);
		return base.OnApply(p);
	}
    public override void OnRemove(PlayerEffects p)
	{

	}
}
