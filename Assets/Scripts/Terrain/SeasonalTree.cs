using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonalTree : SeasonalObject
{
	public Material springMat;
	public Material summerMat;
	public Material fallMat;
	public Material winterMat;

	private MeshRenderer meshRenderer;

	void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	protected override void Spring()
	{
		StartCoroutine(ChangeColor(springMat));
	}
	protected override void Summer()
	{
		StartCoroutine(ChangeColor(summerMat));
	}
	protected override void Fall()
	{
		StartCoroutine(ChangeColor(fallMat));
	}
	protected override void Winter()
	{
		StartCoroutine(ChangeColor(winterMat));
	}

	IEnumerator ChangeColor(Material mat)
	{
		meshRenderer.material = mat;
		float timer = 0;
		while(timer < 3f)
		{
			timer += Time.deltaTime;
			meshRenderer.material.SetFloat("_BlendAmt", timer / 3f);
			yield return null;
		}
	}
}
