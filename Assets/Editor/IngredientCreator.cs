using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IngredientCreator : EditorWindow
{
	private Ingredient toCreate;
	private Effect toAdd;
	private int tempPotency;
	private float tempDuration;
	
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
			toCreate.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity:", toCreate.rarity);
			toAdd = EditorGUILayout.ObjectField(toAdd, typeof(Effect), false) as Effect;
			tempDuration = EditorGUILayout.FloatField("Duration:", tempDuration);
			tempPotency = EditorGUILayout.IntField("Potency:", tempPotency);
			if(GUILayout.Button("Add Effect"))
			{
				toCreate.effects.Add(toAdd);
				toCreate.effectData.Add(new EffectData(tempPotency, tempDuration));
				tempDuration = tempPotency = 0;
				toAdd = null;
			}
			EditorGUILayout.LabelField("Effects:");
			EditorGUI.indentLevel++;
			if(toCreate.effects.Count == 0)
			{
				EditorGUILayout.LabelField("Empty");
			}
			foreach(Effect e in toCreate.effects)
			{
				EditorGUILayout.LabelField(e.GetType().Name);
				EditorGUILayout.LabelField("Duration:"+e.maxDuration);
				EditorGUILayout.LabelField("Potency:"+e.potency);
			}
			EditorGUI.indentLevel--;
			

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