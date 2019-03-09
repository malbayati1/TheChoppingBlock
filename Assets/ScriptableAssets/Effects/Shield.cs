using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Effect
{
	// public float currentDuration;
	// public float maxDuration;
	// public float potency

	public bool didEffect;

	public override bool OnApply(PlayerEffects p)
	{
		didEffect = false;
		Debug.Log("subscrib");
		p.stats.health.preDamageEvent += BlockDamage;
		return base.OnApply(p);
	}
	
	public override bool Tick(float deltaTime, PlayerEffects p)
	{
		if(didEffect)
		{
			return true;
		}
		return base.Tick(deltaTime, p);
	}

	public override void OnRemove(PlayerEffects p)
	{
		Debug.Log("unsub");
		p.stats.health.preDamageEvent -= BlockDamage;
	}

	public void BlockDamage(HealthChangeEventData hced)
	{
		if(!hced.cancelled)
		{
			hced.cancelled = true;
			didEffect = true;
		}
	}
}
