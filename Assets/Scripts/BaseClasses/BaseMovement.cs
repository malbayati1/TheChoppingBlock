using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseMovement : MonoBehaviour
{
    [HideInInspector]
    public GameObject animatingCharacter;
    public float maxDistanceFromAnimatingCharacter;
    protected Rigidbody rb;

    protected bool canMove = true;

	protected Vector3 movementDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Move(float xInput, float zInput)
    {
        if (!canMove)
        {
            Debug.Log("Nope");
            animatingCharacter.GetComponent<AnimatedMover>().Move(0f, 0f);
            return;
        }

        float moveHorizontal = xInput;
        float moveVertical = zInput;
		movementDirection = moveVertical * CameraController.instance.forwardDirection + moveHorizontal * CameraController.instance.rightDirection;
        rb.velocity = movementDirection.normalized;
        //this assignment is to work well with being a focus for animation
        //the position of this object is lerped to constantly by the animating thing so its important that
        //to avoid walking through walls, this focus exists at roughly the same height as the player animator so
        //that this object collides with things the player would collide with
        //so focus cannot schloop through walls and let the player follow.
        transform.position = new Vector3(
            transform.position.x,
            animatingCharacter.transform.position.y,
            transform.position.z);
        //teleport the focus back to the player if the focus gets unreasonably far away
        if(Vector3.Distance(transform.position, animatingCharacter.transform.position) > maxDistanceFromAnimatingCharacter)
        {
            transform.position = animatingCharacter.transform.position;
        }

        animatingCharacter.GetComponent<AnimatedMover>().Move(xInput, zInput);
    }

    public void Push(Vector3 impulse)
    {
        rb.AddForce(impulse);

        StartCoroutine(loseControlUntilGrounded());
    }

    protected IEnumerator loseControlUntilGrounded()
    {
        canMove = false;

        while (!animatingCharacter.GetComponent<AnimatedMover>().IsGrounded())
        {
            yield return new WaitForEndOfFrame();
        }

        canMove = true;
    }
}
