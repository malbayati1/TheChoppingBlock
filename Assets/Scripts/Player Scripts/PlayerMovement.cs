using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;
    private Rigidbody rb;

	private Vector3 movementDirection;

    void Awake()
    {
        //stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
		movementDirection = moveVertical * CameraController.instance.forwardDirection + moveHorizontal * CameraController.instance.rightDirection;
        rb.velocity = movementDirection.normalized * stats.movementSpeed.value;
    }
}
