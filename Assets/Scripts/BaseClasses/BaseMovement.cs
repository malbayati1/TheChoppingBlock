using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseMovement : MonoBehaviour
{
	private const float UPDATEDISTANCE = 4f;

    protected bool canMove = true;

	protected Vector3 movementDirection;

    protected NavMeshAgent navMeshAgent;

    [HideInInspector]
    public AnimatedMover mover;

    protected Unit unit;

	protected LayerMask terrain;

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        mover = GetComponentInChildren<AnimatedMover>();
        unit = GetComponent<Unit>();
		terrain = ~LayerMask.NameToLayer("Terrain");
    }

	protected virtual void Update()
	{
		if (!canMove)
        {
            return;
        }
		mover.Move();
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

		float destToTargetDist = (navMeshAgent.destination - target).magnitude;
		float selfToTargetDist = (transform.position - target).magnitude;
		if( destToTargetDist >= UPDATEDISTANCE || ( selfToTargetDist >= float.Epsilon && selfToTargetDist <= UPDATEDISTANCE) )
		{
			//Debug.Log("setting");
        	navMeshAgent.destination = target;
		}
		if(navMeshAgent.pathPending && navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
		{
			//Debug.Log("no path?");
			navMeshAgent.Move((target - transform.position).normalized * navMeshAgent.speed * Time.deltaTime);
		}
		
    }

    public void Push(Vector3 impulse)
    {
        impulse *= 2f;
        //navMeshAgent.destination = transform.position + new Vector3(impulse.x, 0f, impulse.z);
        float time = unit.hitImmunityCoolDown / 2;

        Vector3 offset = new Vector3(impulse.x, 0f, impulse.z);

        Vector3 destination = transform.position + offset;

        RaycastHit hit;
		NavMeshHit navMeshHit;
        if (Physics.Raycast(transform.position, offset, out hit, offset.magnitude, terrain))
        {
			if (NavMesh.SamplePosition(hit.point, out navMeshHit, 5.0f, NavMesh.AllAreas)) 
			{
				destination = navMeshHit.position;
			}
			else
			{
            	destination = hit.point;
            	destination.y = 0;
			}
        }
		else
		{
			if (NavMesh.SamplePosition(destination, out navMeshHit, 5.0f, NavMesh.AllAreas)) 
			{
				destination = navMeshHit.position;
			}
		}

        

        iTween.MoveTo(gameObject, iTween.Hash("position", destination, "easeType", "easeOutExpo", "time", time));
        
        StartCoroutine(mover.Arc(impulse.y, time));

        StartCoroutine(loseControlForSeconds(time * 1.5f));
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
