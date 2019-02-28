using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[CreateAssetMenu(fileName = "RECIPELIST", menuName = "ScriptableAssets/RECIPELIST")]
public class RecipeList : ScriptableObject
{
	public List<Mixture> mixtures;//= new List<Mixture>();
}
