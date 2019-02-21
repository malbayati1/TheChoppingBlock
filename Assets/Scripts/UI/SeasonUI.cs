using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonUI : MonoBehaviour
{
    public Sprite summerSprite;
	public Sprite springSprite;
	public Sprite fallSprite;
	public Sprite winterSprite;

	private Image seasonImage;
	private Text seasonText;
	private Text seasonTimerText;

	private float localTimer;

	void Awake()
	{
		seasonImage = transform.Find("SeasonIcon").gameObject.GetComponent<Image>();
		seasonText = transform.Find("SeasonText").gameObject.GetComponent<Text>();
		seasonTimerText = transform.Find("TimerText").gameObject.GetComponent<Text>();
		//HandleSeasonChange(SeasonManager.instance.GetCurrentSeason());
	}

    void OnEnable()
    {
        SeasonManager.instance.seasonChangeEvent += HandleSeasonChange;
    }

	void OnDisable()
	{
		SeasonManager.instance.seasonChangeEvent -= HandleSeasonChange;
	}

	void Update()
	{
		localTimer -= Time.deltaTime;
		seasonTimerText.text = (localTimer >= 10) ? string.Format("{0}:{1:D2}", (int)localTimer / 60, (int)localTimer % 60)  : string.Format("{0:F2}", localTimer);
	}

	void HandleSeasonChange(Season s)
	{
		localTimer = SeasonManager.instance.seasonTimer;
		switch(s)
		{
			case Season.Spring:
			{
				seasonImage.sprite = springSprite;
				seasonText.text = "Spring";
			} break;

			case Season.Summer:
			{
				seasonImage.sprite = summerSprite;
				seasonText.text = "Summer";
			} break;

			case Season.Fall:
			{
				seasonImage.sprite = fallSprite;
				seasonText.text = "Fall";
			} break;

			case Season.Winter:
			{
				seasonImage.sprite = winterSprite;
				seasonText.text = "Winter";
			} break;

			default:
			{

			} break;
		}
	}
}
