using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IngredientCreator : EditorWindow
{
	private Ingredient toCreate;
	
	[MenuItem("Window/IngredientCreator")]
	public static void Init()
	{
		GetWindow(typeof(IngredientCreator));
	}

	void OnGUI()
	{
		if(EditorApplication.isPlayingOrWillChangePlaymode)
		{
			return;
		}
		if(toCreate == null)
		{
			toCreate = ScriptableObject.CreateInstance<Ingredient>();
			return;
		}
		else
		{
			toCreate.name = EditorGUILayout.TextField("Name:", toCreate.name);

			// EditorGUILayout.BeginHorizontal();
			// newIngredient = EditorGUILayout.ObjectField(newIngredient, typeof(Ingredient), false) as Ingredient;
			// if(GUILayout.Button("Add"))
			// {
			// 	toCreate.ingredients.Add(newIngredient);
			// 	newIngredient = null;
			// }
			// EditorGUILayout.EndHorizontal();

			// EditorGUILayout.LabelField("Current Ingredients");
			// EditorGUI.indentLevel++;
			// foreach(Ingredient i in toCreate.ingredients)
			// {
			// 	EditorGUILayout.LabelField(i.name);
			// }
			// EditorGUI.indentLevel++;

			if(GUILayout.Button("Create") && toCreate != null)
			{
				AssetDatabase.CreateAsset(toCreate, "Assets/ScriptableAssets/Ingredients/" + toCreate.name + ".asset");
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