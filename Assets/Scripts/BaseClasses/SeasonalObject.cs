using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SeasonalObject : MonoBehaviour
{
	protected virtual void Start()
	{
		HandleSeasonChange(SeasonManager.instance.GetCurrentSeason());
	}

    protected virtual void OnEnable()
	{	
		SeasonManager.instance.seasonChangeEvent += HandleSeasonChange;
	}
	
	protected virtual void OnDisable()
	{
		SeasonManager.instance.seasonChangeEvent -= HandleSeasonChange;
	}

	public virtual void HandleSeasonChange(Season s)
	{
		switch(s)
		{
			case Season.Spring:
			{
				Spring();
			} break;

			case Season.Summer:
			{
				Summer();
			} break;

			case Season.Fall:
			{
				Fall();
			} break;

			case Season.Winter:
			{
				Winter();
			} break;
		}
	}

	protected abstract void Spring();
	protected abstract void Summer();
	protected abstract void Fall();
	protected abstract void Winter();
	// protected override void Spring()
	// {

	// }
	// protected override void Summer()
	// {

	// }
	// protected override void Fall()
	// {

	// }
	// protected override void Winter()
	// {
		
	// }
}
