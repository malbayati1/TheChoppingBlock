using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMoveSpeed : Effect
{
    // public float currentDuration;
	// public float maxDuration;
	// public float potency

    public override bool OnApply(PlayerEffects p)
	{
		Debug.Log("APPLYING INCREASE MOVE SPEED");
		//p.stats.movementSpeed.AddMultiplicativeModifier(1 + potency * 0.5f);
		p.stats.movementSpeed.AddAdditiveModifier(potency);
		return base.OnApply(p);
	}
    public override void OnRemove(PlayerEffects p)
	{
		Debug.Log("REMOVING INCREASE MOVE SPEED");
		//p.stats.movementSpeed.RemoveMultiplicativeModifier(1 + potency * 0.5f);
		p.stats.movementSpeed.RemoveAdditiveModifier(potency);
	}
}
