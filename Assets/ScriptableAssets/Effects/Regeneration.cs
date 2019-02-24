using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Effect
{
	// public float currentDuration;
	// public float maxDuration;
	// public float potency

	private float timePerHeal;
	private float timer;
	private int remainderHeal;

	public override bool OnApply(PlayerEffects p)
	{
		timePerHeal = maxDuration / potency;
		timer = 0;
		remainderHeal = (int)potency;
		return base.OnApply(p);
	}

    public override bool Tick(float deltaTime, PlayerEffects p)
	{
		timer += deltaTime;
		if(timer >= timePerHeal)
		{
			--remainderHeal;
			timer = 0;
			Debug.Log("healing 1");
			p.stats.health.Heal(1);
		}
		return base.Tick(deltaTime, p);
	}

	public override void OnRemove(PlayerEffects p)
	{
		Debug.Log("healing " + remainderHeal);
		p.stats.health.Heal(remainderHeal);
	}
}
