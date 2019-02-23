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
	private Health hp;

	public override bool OnApply(PlayerEffects p)
	{
		timePerHeal = maxDuration / potency;
		timer = 0;
		remainderHeal = (int)potency;
		hp = p.gameObject.GetComponent<Health>();
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
			hp.Heal(1);
		}
		return base.Tick(deltaTime, p);
	}

	public override void OnRemove(PlayerEffects p)
	{
		Debug.Log("healing " + remainderHeal);
		hp.Heal(remainderHeal);
	}
}
