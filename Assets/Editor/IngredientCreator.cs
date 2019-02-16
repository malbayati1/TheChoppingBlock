using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RecipeCreator : EditorWindow
{
    private Mixture toCreate;
    
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
            toCreate.name = EditorGUILayout.DelayedTextField("Name:", toCreate.name);

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