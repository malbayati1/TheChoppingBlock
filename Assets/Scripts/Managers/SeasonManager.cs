using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season { Spring, Summer, Fall, Winter };

public class SeasonManager : Singleton<SeasonManager>
{
    public float seasonTimer;

    public delegate void SeasonChangeDelegate(Season s);
    public event SeasonChangeDelegate seasonChangeEvent = delegate {};

    private float timer;
    private Season currentSeason;

    void Start()
    {
        currentSeason = Season.Spring;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > seasonTimer)
        {
            timer = 0;
            ChangeSeason();
        }
    }

    void ChangeSeason()
    {
        currentSeason++;
        seasonChangeEvent(currentSeason);
    }
}
