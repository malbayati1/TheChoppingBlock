using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectIconUI : MonoBehaviour
{
    private Effect representing;

	private Image icon;
	private Text timer;

	void Awake()
	{
		this.enabled = false;
		icon = transform.GetChild(2).GetComponent<Image>();
		timer = transform.GetChild(1).GetComponent<Text>();
	}

	public void SetEffect(Effect e)
	{
		representing = e;
		icon.sprite = e.icon;
		timer.text = string.Format("{0:0.00}/{1}", e.currentDuration, e.maxDuration);
		this.enabled = true;
	}


	void Update()
	{
		timer.text = string.Format("{0:0.00}/{1}", representing.currentDuration, representing.maxDuration);
		if(representing.currentDuration <= 0)
		{
			Destroy(gameObject);
		}
	}

}
