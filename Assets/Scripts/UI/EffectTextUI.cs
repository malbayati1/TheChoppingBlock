using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectTextUI : MonoBehaviour
{
	public float speed;
	public float timeAlive;

	[HideInInspector] public Vector3 effectTextBaseLocation;
	[HideInInspector] public Vector3 axis;

	private float offset;
	private float timer;
	private Text text;
	private Vector3 savedLocation;

	void Awake()
	{
		offset = 0;
		text = GetComponent<Text>();
		timer = 0;
	}

	public void SetText(string s)
	{
		text.text = s;
	}

    void Update()
    {
		offset += speed * Time.deltaTime;
		timer += Time.deltaTime;
		transform.position = Camera.main.WorldToScreenPoint(effectTextBaseLocation + axis * offset);
		text.color =  new Color(text.color.r, text.color.g, text.color.b,  1 - timer / timeAlive);
		if(timer >= timeAlive)
		{
			Destroy(gameObject);
		}
    }
}
