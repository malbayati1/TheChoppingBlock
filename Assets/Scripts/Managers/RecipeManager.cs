using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RecipeManager : Singleton<RecipeManager>
{
    public GameObject defaultResult;
	public RecipeList recipeList;

    private Dictionary<Mixture, GameObject> recipes;
    public delegate void CookEvent(Ingredient createdFood);
    public event CookEvent onCook = delegate { };



    void Start()
    {
		recipes = new Dictionary<Mixture, GameObject>();
		// string[] allRecipes = AssetDatabase.FindAssets("t:Mixture", new [] {"Assets/ScriptableAssets/Recipes"});
		// foreach(string s in allRecipes)
		// {
		// 	Mixture m = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(Mixture)) as Mixture;
		// 	m = Object.Instantiate(m) as Mixture;
		// 	m.OrderSelf();
		// 	GameObject temp = m.result;
		// 	m.result = null;
		// 	//Debug.Log(m.GetHashCode());
		// 	recipes[m] = temp;
		// 	//recipes.Add(m, temp);
		// }
        // //load our asset database into the dictionary
		Mixture inst;
		recipeList = Object.Instantiate(recipeList);
		foreach(Mixture m in recipeList.mixtures)
		{
			//Debug.Log(m);
			inst = Object.Instantiate(m) as Mixture;
			//Debug.Log(inst);
			inst.OrderSelf();
			GameObject temp = inst.result;
			//Debug.Log(temp);
			inst.result = null;
			recipes[inst] = temp;
		}
    }

	//trys to get a value by sorting our mixtures and hashing them into the dictionary
	//if we fail we return a default
    public GameObject GetResult(Mixture m)
    {
		
		m.OrderSelf();
		//Debug.Log(m);
		//Debug.Log(m.GetHashCode());
        GameObject ret;
        if(recipes.TryGetValue(m, out ret))
        {
			GameObject temp = Instantiate(ret, Vector3.one * 9999, Quaternion.identity);
            onCook(temp.GetComponent<InGameIngredient>().ingredientData);
			// if(m.useOverrides)
			// {
			// 	InGameIngredient ingredient = temp.GetComponent<InGameIngredient>();
			// 	ingredient.ingredientData.potency = m.overridePotency;
			// 	ingredient.ingredientData.duration = m.overrideDuration;
			// } COME BACK TO OVERRIDES LATER IF NEEDED
            return temp;
        }
        else
        {
            GameObject temp =  Instantiate(defaultResult, Vector3.one * 9999, Quaternion.identity);
            onCook(temp.GetComponent<InGameIngredient>().ingredientData);
            return temp;
        }
    }
}


