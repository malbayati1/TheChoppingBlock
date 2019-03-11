using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : BaseMovement
{
	public bool doTrail;
	public Vector3 trailPosition;

	private const float UPDATEDISTANCE = 4f;

    private PlayerStats stats;
	private Vector3 lastPathPosition;
	private NavMeshPath navMeshPath;
	private LineRenderer trail;
	private int endIndex;

    protected override void Awake()
    {
		base.Awake();
        stats = GetComponent<PlayerStats>();
		navMeshPath = new NavMeshPath();
		lastPathPosition = Vector3.one * 9999;
		trail = transform.GetChild(4).gameObject.GetComponent<LineRenderer>();
    }

	void Start()
	{
		SetMoveSpeed(stats.movementSpeed.Value);
	}

	void OnEnable()
	{
		stats.movementSpeed.statChangeEvent += SetMoveSpeed;
	}

	void OnDisable()
	{
		stats.movementSpeed.statChangeEvent -= SetMoveSpeed;
	}

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        
        Vector2 moveInput = new Vector2(moveHorizontal, moveVertical).normalized;

		Move(moveInput.x, moveInput.y);
		if(doTrail)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				UpdatePath(true);
				return;
			}
			else if(Input.GetKey(KeyCode.Space))
			{
				UpdatePath(false);
				return;
			}
			else
			{
				trail.positionCount = 0;
			}
		}
    }

    public override void Move(float xInput, float zInput)
    {
        //xInput *= stats.movementSpeed.value;
        //zInput *= stats.movementSpeed.value;
        //base.Move(xInput, zInput);
		if(xInput != 0 || zInput != 0)
		{
			Vector3 target = new Vector3(xInput, 0, zInput).normalized;
			transform.LookAt(transform.position + target, Vector3.up);
			navMeshAgent.Move(target * Time.deltaTime * navMeshAgent.speed);
			mover.Move();
		}
    }

	void SetMoveSpeed(float speed)
	{
		navMeshAgent.speed = speed;
	}

	void UpdatePath(bool recalc)
	{
		if(recalc || (lastPathPosition - transform.position).magnitude >= UPDATEDISTANCE)
		{
			Debug.Log("setting");
			NavMesh.CalculatePath(trailPosition, transform.position, NavMesh.AllAreas, navMeshPath);
			lastPathPosition = transform.position;
			trail.positionCount = navMeshPath.corners.Length;
			trail.SetPositions(navMeshPath.corners);
			endIndex = navMeshPath.corners.Length - 1;
		}
		else
		{
			if(endIndex < trail.positionCount)
			{
				trail.SetPosition(endIndex, transform.position);
				trail.material.SetTextureOffset("_MainTex", new Vector2(Time.time, 0));
			}
		}
	}
}
