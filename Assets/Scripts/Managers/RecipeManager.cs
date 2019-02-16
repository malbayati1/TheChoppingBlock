using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : Singleton<RecipeManager>
{
    public Ingredient defaultResult;

    private Dictionary<Mixture, Ingredient> recipes;

    void Start()
    {
        //load our asset database into the dictionary
    }

    public Ingredient GetResult(Mixture m)
    {
        Ingredient ret;
        if(recipes.TryGetValue(m, out ret))
        {
            return ret;
        }
        else
        {
            return defaultResult;
        }
    }
}
