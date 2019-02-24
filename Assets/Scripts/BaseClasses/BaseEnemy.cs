using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Unit
{
    public int damageModifier = 1;
    public int knockbackModifier = 1;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform, Vector3.up);
        GetComponent<BaseMovement>().Move(player.transform.position);
    }
}
