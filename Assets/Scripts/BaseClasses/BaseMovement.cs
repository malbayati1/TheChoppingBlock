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

    protected Unit unit;

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        mover = GetComponentInChildren<AnimatedMover>();
        unit = GetComponent<Unit>();
    }

    public virtual void Move(float xInput, float zInput)
    {
        if (xInput != 0 || zInput !=0)
        {
            Vector3 delta = new Vector3(xInput, 0f, zInput);
            Move(transform.position + delta, delta); 
        }
        else
        {
            navMeshAgent.destination = transform.position;
        }

        if (transform.position.y < -1 || mover.transform.position.y < -1)
        {
            Destroy(gameObject);
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
        mover.Move();
    }

    public void Push(Vector3 impulse)
    {
        impulse *= 2f;
        //navMeshAgent.destination = transform.position + new Vector3(impulse.x, 0f, impulse.z);
        float time = unit.hitImmunityCoolDown;
        //mover.Move(0f, 0f, impulse.y);
        iTween.MoveTo(gameObject, iTween.Hash("position", transform.position + new Vector3(impulse.x, 0f, impulse.z), "easeType", "easeOutExpo", "time", time));
        
        StartCoroutine(mover.Arc(impulse.y, time));

        StartCoroutine(loseControlForSeconds(time));
    }

    protected IEnumerator loseControlForSeconds(float delay)
    {
        canMove = false;
		yield return new WaitForSeconds(delay);
        canMove = true;
    }

    protected bool IsGrounded()
    {
        return mover.IsGrounded();
    }
}
