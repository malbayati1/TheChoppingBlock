using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    private PlayerStats stats;

    // Start because some things are not  ready by Awake
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        
        Vector2 moveInput = new Vector2(moveHorizontal, moveVertical).normalized;

		Move(moveInput.x, moveInput.y);
    }

    public override void Move(float xInput, float zInput)
    {
        //xInput *= stats.movementSpeed.value;
        //zInput *= stats.movementSpeed.value;
        base.Move(xInput, zInput);
    }
}
