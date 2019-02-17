using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableAssets/Ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
	public string name;
	public int ID;
	//public Sprite sprite;
    [SerializeField] public Effect effect;
    public float potency = 1;
    public float duration;   
    public Rarity rarity;
    [HideInInspector]public bool isPreserved = false;

	public override string ToString()
	{
		string ret = string.Format("ID:{0}\nname:{1}\ndur:{2}\npot:{3}", ID, name, duration, potency);
		return ret;
	}
}
