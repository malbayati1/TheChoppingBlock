using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixture : ScriptableObject
{
    public List<InGameIngredient> ingredients = new List<InGameIngredient>();
    public void OrderSelf()
    {
        ingredients.Sort(delegate (InGameIngredient in1, InGameIngredient in2) { return in1.ingredientData.ID - in2.ingredientData.ID; });
    }
    public bool AddIngredient(InGameIngredient i)
    {
        return false;
        //checks if the recipe is valid, if so add to the array, otherwise return false
    }
}
