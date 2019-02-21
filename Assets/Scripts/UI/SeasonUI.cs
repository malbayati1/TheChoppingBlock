using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonUI : MonoBehaviour
{
    public Sprite summerSprite;
	public Sprite springSprite;
	public Sprite fallSprite;
	public Sprite winterSprite;

    void OnEnable()
    {
        SeasonManager.instance.seasonChangeEvent += HandleSeasonChange;
    }

	void OnDisable()
	{
		SeasonManager.instance.seasonChangeEvent += HandleSeasonChange;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void HandleSeasonChange(Season s)
	{
		switch(s)
		{
			case Season.Spring:
			{

			} break;

			default:
			{

			} break;
		}
	}
}
