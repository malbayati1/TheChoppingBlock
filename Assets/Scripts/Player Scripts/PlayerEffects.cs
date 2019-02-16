using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
	public PlayerStats stats;

    private List<Effect> effects;

	void Awake()
	{
		stats = GetComponent<PlayerStats>();
	}

	void Update()
	{
		for(int x = effects.Count; x >= 0; --x)
		{
			if(effects[x].Tick(Time.deltaTime, this))
			{
				effects.RemoveAt(x);
			}
		}
	}

	public void AddEffect(Effect e)
	{
		effects.Add(e);
	}
}
