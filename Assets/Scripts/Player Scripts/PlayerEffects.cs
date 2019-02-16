using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
	[HideInInspector] public PlayerStats stats;

    private List<Effect> effects;

	void Awake()
	{
		effects = new List<Effect>();
		stats = GetComponent<PlayerStats>();
	}

	void Update()
	{
		for(int x = effects.Count - 1; x >= 0; --x)
		{
			if(effects[x].Tick(Time.deltaTime, this))
			{
				effects.RemoveAt(x);
			}
		}
	}

	public void AddEffect(Effect e)
	{
		if(!e.OnApply(this))
		{
			effects.Add(e);
		}
	}
}
