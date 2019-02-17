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
			toCreate.ID = EditorGUILayout.IntField("ID:", toCreate.ID);
			toCreate.effect = EditorGUILayout.ObjectField(toCreate.effect, typeof(Effect), false) as Effect;
			toCreate.potency = EditorGUILayout.FloatField("Potency:", toCreate.potency);
			toCreate.duration = EditorGUILayout.FloatField("Duration:", toCreate.duration);
			toCreate.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity:", toCreate.rarity);

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