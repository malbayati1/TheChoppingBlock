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
			toCreate.effect = EditorGUILayout.ObjectField(toCreate.effect, typeof(Effect), false) as Effect;
			toCreate.potency = EditorGUILayout.FloatField("Potency:", toCreate.potency);
			toCreate.duration = EditorGUILayout.FloatField("Duration:", toCreate.duration);
			toCreate.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity:", toCreate.rarity);

			// if(GUILayout.Button("Test"))
			// {
			// 	Debug.Log("ID:" + GetUniqueID());
			// }

			if(GUILayout.Button("Create") && toCreate != null)
			{
				toCreate.ID = GetUniqueID();
				AssetDatabase.CreateAsset(toCreate, "Assets/ScriptableAssets/Ingredients/" + toCreate.name + ".asset");
				Debug.Log("Creating " + toCreate.name + " with ID " + toCreate.ID);
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

	private int GetUniqueID()
	{
		string[] ingredientPaths = AssetDatabase.FindAssets("t:Ingredient");
		List<int> alreadyUsedIDs = new List<int>();
		foreach(string s in ingredientPaths)
		{
			alreadyUsedIDs.Add(((Ingredient)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(Ingredient))).ID);
		}
		int ID = int.MinValue;
		foreach(int i in alreadyUsedIDs)
		{
			if(i > ID)
			{
				ID = i;
			}
		}
		return ++ID;
	}
}