using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float value;

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
        value = finalResult;
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
