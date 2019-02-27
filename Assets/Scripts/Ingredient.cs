using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableAssets/Ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
	[SerializeField] 
	public List<Effect> effects = new List<Effect>();
	[SerializeField]
	public List<EffectData> effectData = new List<EffectData>();

	public string name;
	public int ID; 
    public Rarity rarity;
    [HideInInspector] public bool isPreserved = false;

	public override string ToString()
	{
		string ret = string.Format("ID:{0}\nname:{1}\n", ID, name);
		foreach(Effect e in effects)
		{
			ret += string.Format("effect:{0}\ndur:{1}\npot:{2}", e.GetType().Name, e.maxDuration, e.potency);
		}
		return ret;
	}

	public override bool Equals(object other)
	{
		if(other == null)
		{
			return false;
		}
		return ID == ((Ingredient)other).ID;
	}
	
	public override int GetHashCode()
	{
		return name.GetHashCode();
	}
}
