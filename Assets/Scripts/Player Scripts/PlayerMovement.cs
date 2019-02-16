using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats stats;
    private Rigidbody2D rigidbody;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        rigidbody.velocity = new Vector2(moveHorizontal, moveVertical).normalized * stats.movementSpeed.value;
    }
}
