using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseMovement : MonoBehaviour
{
    protected bool canMove = true;

	protected Vector3 movementDirection;

    protected NavMeshAgent navMeshAgent;

    protected AnimatedMover mover;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        mover = GetComponentInChildren<AnimatedMover>();
    }

    public virtual void Move(float xInput, float zInput)
    {
        if (xInput != 0 || zInput !=0)
        {
            Vector3 delta = new Vector3(xInput, 0f, zInput);
            Move(transform.position + delta, delta); 
        }
    }

    public virtual void Move(Vector3 target, Vector3 delta = new Vector3())
    {
        if (!canMove)
        {
            return;
        }
        target.y = transform.position.y;
        transform.LookAt(target, Vector3.up);
        navMeshAgent.destination = target;
        mover.Move(delta.x, delta.z);
    }

    public void Push(Vector3 impulse)
    {
        navMeshAgent.destination = transform.position + new Vector3(impulse.x, 0f, impulse.z);

        mover.Move(0f, 0f, impulse.y);

        StartCoroutine(loseControlUntilGrounded());
    }

    protected IEnumerator loseControlUntilGrounded()
    {
        canMove = false;
		yield return new WaitUntil(() => IsGrounded());
        canMove = true;
    }

    protected bool IsGrounded()
    {
        return mover.IsGrounded();
    }
}
