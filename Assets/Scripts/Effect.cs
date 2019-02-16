using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public float currentDuration;
    public float maxDuration;

    public abstract void OnApply();
    public abstract void OnRemove();
    public virtual void Tick(float deltaTime)
    {
        currentDuration -= deltaTime;
        if(currentDuration <= 0)
        {
            OnRemove();
            Destroy(this);
        }
    }
}
