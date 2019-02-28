using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectData
{
	public int potency;
	public float duration;
	public EffectData(int p, float d)
	{
		potency = p;
		duration = d;
	}
	public EffectData()
	{

	}
}