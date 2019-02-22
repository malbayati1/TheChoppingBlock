using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : BaseMovement
{
    private PlayerStats stats;

    // Start because some things are not  ready by Awake
    void Start()
    {
        stats = animatingCharacter.GetComponent<PlayerStats>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        //Debug.Log("Heh? "+ moveHorizontal + " hm " +moveVertical);
		Move(moveHorizontal, moveVertical);
    }

    public override void Move(float xInput, float zInput)
    {
        base.Move(xInput, zInput);
        //Debug.Log(rb.velocity);
        rb.velocity *= stats.movementSpeed.value;
    }
}
