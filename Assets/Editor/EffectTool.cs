using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EffectTool : EditorWindow
{
    private Mixture toCreate;
    
    [MenuItem("Window/EffectImporter")]
    public static void Init()
    {
        GetWindow(typeof(EffectTool));
    }

    void OnGUI()
    {
		if(GUILayout.Button("Reimport"))
		{
			string[] effectPaths = AssetDatabase.FindAssets("t:Effect");
			List<string> alreadyCreatedAssets = new List<string>();
			foreach(string s in effectPaths)
			{
				alreadyCreatedAssets.Add(  ((Effect)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(Effect))).name   );
			}

			string[] allEffectsScriptsNames = AssetDatabase.FindAssets("", new [] {"Assets/ScriptableAssets/Effects"});
			foreach(string s in allEffectsScriptsNames)
			{
				string effectName = AssetDatabase.GUIDToAssetPath(s).Substring(AssetDatabase.GUIDToAssetPath(s).LastIndexOf('/') + 1);
				effectName = effectName.Substring(0, effectName.LastIndexOf('.'));
				if(alreadyCreatedAssets.Contains(effectName))
				{
					continue;
				}
				ScriptableObject newEffectAsset = ScriptableObject.CreateInstance(effectName);
				AssetDatabase.CreateAsset(newEffectAsset, "Assets/ScriptableAssets/Effects/" + effectName +".asset");
				Debug.Log("Creating new asset: " + effectName + ".asset");
			}
			Debug.Log("Tool running complete");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			return;
		}
	}
}
