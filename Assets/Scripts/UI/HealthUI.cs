using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject emptyHealthPrefab;
	public GameObject halfHealthPrefab;
	public GameObject fullHealthPrefab;
	public GameObject holder;

	private GameObject player;
	private Health playerHealth;
	private int currentHealth;
	private int maxHealth;

	void OnEnable()
	{
		player = GameObject.FindWithTag("Player");
		playerHealth = player.GetComponent<Health>();
		maxHealth = currentHealth = playerHealth.maxHealth;
		playerHealth.changeEvent += UpdateUI;
	}

	void OnDisable()
	{
		playerHealth.changeEvent -= UpdateUI;
	}

    void Start()
    {
		UpdateUI(0);
    }

	// void Update()
	// {
	// 	if(Input.GetKeyDown(KeyCode.K))
	// 	{
	// 		UpdateUI(-1);
	// 	}
	// }

    void UpdateUI(int value)
    {
		currentHealth += value;
		//Debug.Log(currentHealth + "/" + maxHealth);
		foreach(Transform child in holder.transform)
		{
			Destroy(child.gameObject);
		}
		for(int x = 0; x < maxHealth; x += 2)
		{
			if((currentHealth - x) / 2f == 0.5f)
			{
				//Debug.Log(string.Format("x:{0} = HALF", x));
				Instantiate(halfHealthPrefab, Vector3.zero, Quaternion.identity, holder.transform);
			}
			else if(currentHealth > x)
			{
				//Debug.Log(string.Format("x:{0} = FULL", x));
				Instantiate(fullHealthPrefab, Vector3.zero, Quaternion.identity, holder.transform);
			}
			else
			{
				//Debug.Log(string.Format("x:{0} = EMPTY", x));
				Instantiate(emptyHealthPrefab, Vector3.zero, Quaternion.identity, holder.transform);
			}
		}
    }
}
