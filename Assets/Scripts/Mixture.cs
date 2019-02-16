using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixture : ScriptableObject
{
    public List<Ingredient> ingredients = new List<Ingredient>();
    public void OrderSelf()
    {
        ingredients.Sort(delegate (Ingredient in1, Ingredient in2) { return in1.ID - in2.ID; });
    }
    public bool AddIngredient(InGameIngredient i)
    {
		//List<InGameIngredient> current = new List<InGameIngredient>(ingredients);
		ingredients.Add(i.ingredientData);
        return true;
        //checks if the recipe is valid, if so add to the array, otherwise return false
    }
}
