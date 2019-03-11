using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUI : MonoBehaviour
{
	public GameObject bar;
	public GameObject iconPrefab;
	public GameObject effectTextPrefab;
	public float timeBetweenSpawns = 0.5f;

	private float timeSinceLastSpawn;

	private GameObject player;
	private GameObject effectTextLocation;
	private PlayerEffects playerEffects;
	private PlayerInteraction playerInteraction;
	private List<Effect> toSpawnText;

	void OnEnable()
	{
		timeSinceLastSpawn = 0;
		player = GameObject.FindWithTag("Player");
		effectTextLocation = player.transform.GetChild(1).gameObject;
		playerEffects = player.GetComponent<PlayerEffects>();
		playerInteraction = player.GetComponent<PlayerInteraction>();
		toSpawnText = new List<Effect>();
		playerEffects.effectAddedEvent += SpawnEffectIcon;
		playerInteraction.ingredientEatEvent += CreateFakeEffect;
	}

	void OnDisable()
	{
		playerEffects.effectAddedEvent -= SpawnEffectIcon;
		playerInteraction.ingredientEatEvent += CreateFakeEffect;
	}

	void Update()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if(toSpawnText.Count == 0 || timeSinceLastSpawn < timeBetweenSpawns)
		{
			return;
		}
		timeSinceLastSpawn = 0;
		SpawnEffectText(toSpawnText[0]);
		toSpawnText.RemoveAt(0);
	}

	void SpawnEffectIcon(Effect e)
	{
		GameObject temp = Instantiate(iconPrefab, Vector3.zero, Quaternion.identity, bar.transform);
		temp.GetComponent<EffectIconUI>().SetEffect(e);
		//spawned.Add(temp.GetComponent<EffectIconUI>());
	}

	void AddToQueue(Effect e)
	{
		//Debug.Log("adding "+ e);
		toSpawnText.Add(e);
	}

	void CreateFakeEffect(GameObject g)
	{
		Effect temp = ScriptableObject.CreateInstance(typeof(Effect)) as Effect;
		InGameIngredient igi = g.GetComponent<InGameIngredient>();
		temp.description = igi.ingredientData.name;
		AddToQueue(temp);
		foreach(Effect e in igi.ingredientData.effects)
		{
			AddToQueue(e);
		}
	}

	void SpawnEffectText(Effect e)
	{
		GameObject effectText = Instantiate(effectTextPrefab, Vector3.zero, Quaternion.identity, transform);
		EffectTextUI text = effectText.GetComponent<EffectTextUI>();
		text.effectTextBaseLocation = effectTextLocation.transform.position;
		text.axis = CameraController.instance.gameObject.transform.up;
		text.SetText(e.description);
	}
}
