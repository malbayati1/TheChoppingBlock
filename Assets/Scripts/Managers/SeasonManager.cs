﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season { Spring, Summer, Fall, Winter };

public class SeasonManager : Singleton<SeasonManager>
{
    public float seasonTimer;   //Should be greater than 0 so the season doesn't change
                                //right away

    public delegate void SeasonChangeDelegate(Season s);
    public delegate void AlmostSeasonChangeDelegate();
    public event SeasonChangeDelegate seasonChangeEvent = delegate {};
    public event AlmostSeasonChangeDelegate almostSeasonChangeEvent = delegate {};

    private float timer;    //MA 2/21: timer will continue until the game ends.
                            //Used to see how long player lasts. May need to change to mintues
    private float resetTimer;   //Keeps track of the original timer
    private Season currentSeason;
    private int seasonCount; // MA 3/19: Keeps count of how many seasons have gone by

    private bool almostSeasonChangeEventDone = false;

    void Start()
    {
        currentSeason = Season.Spring;
        resetTimer = seasonTimer;
        seasonCount = 0;
    }

    // Muhammad 2/21/2019: Changed the timer
    void Update()
    {
        timer += Time.deltaTime; 
        seasonTimer -= Time.deltaTime;

        if(seasonTimer <= 0)
        {
            seasonTimer = resetTimer;
            ++seasonCount;
            ChangeSeason();
            almostSeasonChangeEventDone = false;
        }
        else if (seasonTimer <= 3f && !almostSeasonChangeEventDone)
        {
            almostSeasonChangeEvent();
            almostSeasonChangeEventDone = true;
        }
    }

    void ChangeSeason()
	{
        currentSeason++;
		if((int)currentSeason == Enum.GetNames(typeof(Season)).Length)
		{
			currentSeason = 0;
		}
        seasonChangeEvent(currentSeason);
    }



	public Season GetCurrentSeason()
	{
		return currentSeason;
	}

    public int getSeasonCount()
    {
        return seasonCount;
    }
}
