using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingredient : MonoBehaviour, IHoldable
{
    public int ID;
    public Effect effect;
    public float potency;
    public float duration;   
    public Rarity rarity;
    public bool isPreserved = false;

    public abstract void Use();
    public abstract void Drop();
}
