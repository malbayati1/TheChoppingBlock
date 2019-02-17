using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixture : ScriptableObject
{
    public List<Ingredient> ingredients = new List<Ingredient>();
	public GameObject result;
	
    public void OrderSelf()
    {
        ingredients.Sort(delegate (Ingredient in1, Ingredient in2) { return in1.ID - in2.ID; });
    }
    public bool AddIngredient(InGameIngredient i)
    {
		ingredients.Add(i.ingredientData);
        return true;
        //checks if the recipe is valid, if so add to the array, otherwise return false
    }

	public override string ToString()
	{
		OrderSelf();
		if(ingredients.Count == 0)
		{
			return "Empty mixture";
		}
		string ret = ingredients[0].name;
		for(int x = 1; x < ingredients.Count; ++x)
		{
			ret += ", " + ingredients[x].name;
		}
		ret += " = " + ((result == null) ? "null" : result.name);
		return ret;
	}
	
	//iterates through all the ingredietns and compares them
	public override bool Equals(object other)
	{
		if(other == null)
		{
			return false;
		}
		Mixture m = (Mixture)other;
		if(ingredients.Count != m.ingredients.Count)
		{
			return false;
		}
		OrderSelf();
		m.OrderSelf();
		for(int x = 0; x < ingredients.Count; ++x)
		{
			if(!ingredients[x].Equals(m.ingredients[x]))
			{
				return false;
			}
		}
		return true;
	}

	public override int GetHashCode()
	{
		return ToString().GetHashCode();
	}
	
}
