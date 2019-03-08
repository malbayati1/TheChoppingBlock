using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
	private GameObject roof;

	private List<Material> roofMats;
	private bool inHouse;
	private float ratio;
	private Color maxOpacityColor;
	private Color fullyTransparentColor;

	void Awake()
	{
		inHouse = true;
		ratio = 0;
		roof = transform.GetChild(1).gameObject;
		roofMats = new List<Material>();
		foreach(Transform child in roof.transform)
		{
			roofMats.Add(child.gameObject.GetComponent<MeshRenderer>().material);
		}
		maxOpacityColor = new Color(roofMats[0].color.r, roofMats[0].color.g, roofMats[0].color.b, 1);
		fullyTransparentColor = new Color(roofMats[0].color.r, roofMats[0].color.g, roofMats[0].color.b, 0);
	}

	void Update()
	{
		if((inHouse && ratio == 0) || (!inHouse && ratio == 1))
		{
			return;
		}
		if(inHouse)
		{
			ratio -= Time.deltaTime;
			ratio = Mathf.Clamp(ratio, 0, 1);
		}
		else
		{
			ratio += Time.deltaTime;
			ratio = Mathf.Clamp(ratio, 0, 1);
		}
		foreach(Material m in roofMats)
		{
			m.color = Color.Lerp(fullyTransparentColor, maxOpacityColor, ratio);
		}		
	}

    public void Enter()
	{
		inHouse = true;
	}

	public void Exit()
	{
		inHouse = false;
	}
}
