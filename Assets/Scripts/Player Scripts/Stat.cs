using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class that I'm using to keep track of player statistics and modifiers
//simplifies adding additive and multiplicative buffs, so they should stack nicely


[System.Serializable]
public class Stat
{
	//USE ME TO ACCESS CURRENT FINAL VALUE
	//SHOULD BE KEPT UPDATED AS BONUSES ARE ADDED AND REMOVED
    public float Value
	{
		get
		{
			return _value;
		}
	}
	private float _value;

	public delegate void StatChangeDelegate(float value);
	public event StatChangeDelegate statChangeEvent = delegate {};

    private float baseValue;
    private List<float> multiplicativeModifiers = new List<float>();
    private List<float> additiveModifiers = new List<float>();

    private void UpdateCurrentValue()
    {
        float finalResult = baseValue;
        foreach(float f in additiveModifiers)
        {
            finalResult += f;
        }
        foreach(float f in multiplicativeModifiers)
        {
            finalResult *= f;
        }
        _value = finalResult;
		statChangeEvent(_value);
    }

    public void SetBaseValue(float f)
    {
        baseValue = f;
        UpdateCurrentValue();
    }

    public void AddAdditiveModifier(float f)
    {
        additiveModifiers.Add(f);
        UpdateCurrentValue();
    }

    public void AddMultiplicativeModifier(float f)
    {
        multiplicativeModifiers.Add(f);
        UpdateCurrentValue();
    }
    
    public void RemoveAdditiveModifier(float f)
    {
        additiveModifiers.Remove(f);
        UpdateCurrentValue();
    }

    public void RemoveMultiplicativeModifier(float f)
    {
        multiplicativeModifiers.Remove(f);
        UpdateCurrentValue();
    }
}
