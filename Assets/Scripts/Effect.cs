using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public float currentDuration;
    public float maxDuration;
	public float potency = 1;

	//returns true if it should be destroyed (one shot effects/duration 0)
    public virtual bool OnApply(PlayerEffects p)
	{
		if(maxDuration <= 0)
		{
			return true;
		}
		return false;
	}
    public abstract void OnRemove(PlayerEffects p);
	
	//returns true when it should be destroyed
    public virtual bool Tick(float deltaTime, PlayerEffects p)
    {
        currentDuration -= deltaTime;
        if(currentDuration <= 0)
        {
            OnRemove(p);
            return true;
        }
		return false;
    }
}
