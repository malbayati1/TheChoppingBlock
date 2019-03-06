using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUI : MonoBehaviour
{
	public GameObject bar;
	public GameObject iconPrefab;

	private GameObject player;
	private PlayerEffects playerEffects;
	private PlayerStats playerStats;
	//private List<EffectIconUI> spawned;

	void OnEnable()
	{
		player = GameObject.FindWithTag("Player");
		playerEffects = player.GetComponent<PlayerEffects>();
		playerStats = player.GetComponent<PlayerStats>();
		playerEffects.effectAddedEvent += SpawnEffectIcon;
	}

	void OnDisable()
	{
		playerEffects.effectAddedEvent -= SpawnEffectIcon;
	}

	void SpawnEffectIcon(Effect e)
	{
		GameObject temp = Instantiate(iconPrefab, Vector3.zero, Quaternion.identity, bar.transform);
		temp.GetComponent<EffectIconUI>().SetEffect(e);
		//spawned.Add(temp.GetComponent<EffectIconUI>());
	}
}
