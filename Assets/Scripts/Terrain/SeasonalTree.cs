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
		meshRenderer.material = springMat;
	}
	protected override void Summer()
	{
		meshRenderer.material = summerMat;
	}
	protected override void Fall()
	{
		meshRenderer.material = fallMat;
	}
	protected override void Winter()
	{
		meshRenderer.material = winterMat;
	}
}
