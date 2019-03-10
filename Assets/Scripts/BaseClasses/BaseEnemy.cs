using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Unit
{
    public int damageModifier = 1;
    public int knockbackModifier = 1;
    public int framesBetweenUpdates = 1;
    protected GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % framesBetweenUpdates != 0)
            return;
        //transform.LookAt(player.transform, Vector3.up);
        movement.Move(player.transform.position);
    }
}
