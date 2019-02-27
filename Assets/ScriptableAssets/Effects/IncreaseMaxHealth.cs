using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHealth : Effect
{
    // public float currentDuration;
	// public float maxDuration;
	// public float potency

    public override bool OnApply(PlayerEffects p)
	{
		Debug.Log("APPLYING INCREASE MAX HP");
		p.stats.maxHealth.AddAdditiveModifier(potency * 2);
		return base.OnApply(p);
	}
    public override void OnRemove(PlayerEffects p)
	{
		Debug.Log("REMOVING INCREASE MAX HP");
		p.stats.maxHealth.RemoveAdditiveModifier(potency * 2);
	}
}
