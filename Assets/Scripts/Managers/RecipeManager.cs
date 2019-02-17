using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RecipeManager : Singleton<RecipeManager>
{
    public GameObject defaultResult;

    private Dictionary<Mixture, GameObject> recipes;

    void Start()
    {
		string[] allRecipes = AssetDatabase.FindAssets("t:Mixture", new [] {"Assets/ScriptableAssets/Recipes"});
		foreach(string s in allRecipes)
		{
			Mixture m = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(Mixture)) as Mixture;
			Debug.Log(AssetDatabase.GUIDToAssetPath(s));
		}
        //load our asset database into the dictionary
    }

	//trys to get a value by sorting our mixtures and hashing them into the dictionary
	//if we fail we return a default
    public GameObject GetResult(Mixture m)
    {
        GameObject ret;
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
