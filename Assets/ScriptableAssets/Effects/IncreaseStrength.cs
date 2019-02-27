using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStrength : Effect
{
    // public float currentDuration;
    // public float maxDuration;
    // public float potency

    public override bool OnApply(PlayerEffects p)
    {
        Debug.Log("APPLYING INCREASE STRENGTH");
        p.stats.strength.AddMultiplicativeModifier(2 + potency * 0.5f);
        return base.OnApply(p);
    }
    public override void OnRemove(PlayerEffects p)
    {
        Debug.Log("REMOVING INCREASE STRENGTH");
        p.stats.strength.RemoveMultiplicativeModifier(2 + potency * 0.5f);
    }
}
