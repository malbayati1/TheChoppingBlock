using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Unit
{
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(player.transform, Vector3.up);
    }
}
