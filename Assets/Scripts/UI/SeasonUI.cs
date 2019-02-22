using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonUI : SeasonalObject
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
	}

	void Update()
	{
		localTimer -= Time.deltaTime;
		seasonTimerText.text = (localTimer >= 10) ? string.Format("{0}:{1:D2}", (int)localTimer / 60, (int)localTimer % 60)  : string.Format("{0:F2}", localTimer);
	}

	public override void HandleSeasonChange(Season s)
	{
		localTimer = SeasonManager.instance.seasonTimer;
		base.HandleSeasonChange(s);
	}

	protected override void Spring()
	{
		UpdateUI(springSprite, "Spring");
	}

	protected override void Summer()
	{
		UpdateUI(summerSprite, "Summer");
	}

	protected override void Fall()
	{
		UpdateUI(fallSprite, "Fall");
	}

	protected override void Winter()
	{
		UpdateUI(winterSprite, "Winter");
	}

	void UpdateUI(Sprite s, string t)
	{
		seasonImage.sprite = s;
		seasonText.text = t;
	}
}
