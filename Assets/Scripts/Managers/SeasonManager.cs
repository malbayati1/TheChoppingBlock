using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season { Spring, Summer, Fall, Winter };

public class SeasonManager : Singleton<SeasonManager>
{
    public float seasonTimer;   //Should be greater than 0 so the season doesn't change
                                //right away

    public delegate void SeasonChangeDelegate(Season s);
    public event SeasonChangeDelegate seasonChangeEvent = delegate {};

    private float timer;    //MA 2/21: timer will continue until the game ends.
                            //Used to see how long player lasts. May need to change to mintues
    private Season currentSeason;

    void Start()
    {
        currentSeason = Season.Spring;
    }

    // Muhammad 2/21/2019: Changed the timer
    void Update()
    {
        timer += Time.deltaTime; 
        seasonTimer -= Time.deltaTime;
    }

    void ChangeSeason()
    {
        currentSeason++;
        seasonChangeEvent(currentSeason);
    }
}
