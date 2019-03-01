using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Effect : ScriptableObject
{
	public string description;
	public Sprite icon;
    [HideInInspector] public float currentDuration;
	public float maxDuration;
	public int potency;

	//returns true if it should be destroyed (one shot effects/duration 0)
    public virtual bool OnApply(PlayerEffects p)
	{
		if(maxDuration <= 0)
		{
			return true;
		}
		currentDuration = maxDuration;
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