using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats stats;
    private Rigidbody rigidbody;

	private Vector3 movementDirection;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
		movementDirection = moveVertical * CameraController.instance.forwardDirection + moveHorizontal * CameraController.instance.rightDirection;
        rigidbody.velocity = movementDirection.normalized * stats.movementSpeed.value;
    }
}
