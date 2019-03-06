using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
	public GameObject effectTextPrefab;
	public GameObject effectTextLocation;
	public GameObject effectTextHUD;

	public delegate void EffectAddedDelegate(Effect e);
	public event EffectAddedDelegate effectAddedEvent = delegate { };

	[HideInInspector] public PlayerStats stats;

    private List<Effect> effects;

	void Awake()
	{
		effects = new List<Effect>();
		stats = GetComponent<PlayerStats>();
	}


	//every frame we update all of the currently applied effects
	//Effect handles its ticking and OnRemoval effect
	void Update()
	{
		for(int x = effects.Count - 1; x >= 0; --x)
		{
			//Debug.Log("ticking " + effects[x]);
			if(effects[x].Tick(Time.deltaTime, this))
			{
				Debug.Log("removing at index " + x);
				effects.RemoveAt(x);
			}
		}
	}

	//Adds an effect and applies any immediate effects
	public void AddEffect(Effect e)
	{
		GameObject effectText = Instantiate(effectTextPrefab, Vector3.zero, Quaternion.identity, effectTextHUD.transform);
		EffectTextUI text = effectText.GetComponent<EffectTextUI>();
		text.effectTextBaseLocation = effectTextLocation.transform.position;
		text.axis = CameraController.instance.gameObject.transform.up;
		text.SetText(e.description);
		if(!e.OnApply(this))
		{
			effects.Add(e);
			effectAddedEvent(e);
		}
	}
}
