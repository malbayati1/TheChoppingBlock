using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
	public string description;
    [HideInInspector] public float currentDuration;
    [HideInInspector] public float maxDuration;
	[HideInInspector] public int potency;

	//returns true if it should be destroyed (one shot effects/duration 0)
    public virtual bool OnApply(PlayerEffects p)
	{
		if(maxDuration <= 0)
		{
			return true;
		}
		return false;
	}
    public virtual void OnRemove(PlayerEffects p)
	{

	}
	
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

	public override string ToString()
	{
		string ret = string.Format("name:{0}, pot:{1}, dur:{2}/{3}", this.GetType().Name, potency, currentDuration, maxDuration);
		return ret;
	} 
}
