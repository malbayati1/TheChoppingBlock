using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RecipeCreator : EditorWindow
{
    private Mixture toCreate;
	private Ingredient newIngredient;
    
    [MenuItem("Window/RecipeCreator")]
    public static void Init()
    {
        GetWindow(typeof(RecipeCreator));
    }

    void OnGUI()
    {
        if(EditorApplication.isPlayingOrWillChangePlaymode)
        {
			return;
        }
		if(toCreate == null)
		{
			toCreate = ScriptableObject.CreateInstance<Mixture>();
			return;
		}
		else
		{
            toCreate.name = EditorGUILayout.TextField("Name:", toCreate.name);

			EditorGUILayout.BeginHorizontal();
			newIngredient = EditorGUILayout.ObjectField("New Ingredient:", newIngredient, typeof(Ingredient), false) as Ingredient;
			if(GUILayout.Button("Add"))
			{
				toCreate.ingredients.Add(newIngredient);
				newIngredient = null;
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField("Current Ingredients");
			EditorGUI.indentLevel++;
			foreach(Ingredient i in toCreate.ingredients)
			{
				EditorGUILayout.LabelField(i.name);
			}
			EditorGUI.indentLevel--;

			toCreate.result = EditorGUILayout.ObjectField("Result Prefab:", toCreate.result, typeof(GameObject), false) as GameObject;

			// toCreate.useOverrides = EditorGUILayout.Toggle("Use Overrides?", toCreate.useOverrides);
			// toCreate.overridePotency = EditorGUILayout.IntField("OverridePotency:", toCreate.overridePotency); REVISIT OVERRIDES LATER
			// toCreate.overrideDuration= EditorGUILayout.FloatField("OverrideDuration:", toCreate.overrideDuration);

            if(GUILayout.Button("Create") && toCreate != null)
			{
				AssetDatabase.CreateAsset(toCreate, "Assets/ScriptableAssets/Recipes/" + toCreate.name + ".asset");
				toCreate = null;
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
				return;
			}

			if(GUILayout.Button("Reset"))
            {
				toCreate = null;
            }
        }

    }
}