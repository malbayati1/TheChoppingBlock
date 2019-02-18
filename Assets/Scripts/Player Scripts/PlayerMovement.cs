using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject animatingPlayer;
    private PlayerStats stats;
    private Rigidbody rb;

	private Vector3 movementDirection;

    void Awake()
    {
        stats = animatingPlayer.GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
		movementDirection = moveVertical * CameraController.instance.forwardDirection + moveHorizontal * CameraController.instance.rightDirection;
        rb.velocity = movementDirection.normalized * stats.movementSpeed.value;
        //this assignment is to work well with being a focus for animation
        //the position of this object is lerped to constantly by the animating thing so its important that
        //to avoid walking through walls, this focus exists at roughly the same height as the player animator so
        //that this object collides with things the player would collide with
        //so focus cannot schloop through walls and let the player follow.
        transform.position = new Vector3(
            transform.position.x,
            animatingPlayer.transform.position.y,
            transform.position.z);
    }
}
