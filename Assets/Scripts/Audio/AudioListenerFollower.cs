using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerFollower : MonoBehaviour
{
    private GameObject player;

    void Start()
	{
		player = GameObject.Find("Player");
	}

	void Update()
	{
		transform.position = player.transform.position;
	}
}