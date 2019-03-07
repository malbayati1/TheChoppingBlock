using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : BaseMovement
{
	public bool doTrail;
	public Vector3 trailPosition;
	public GameObject trailPrefab;

    private PlayerStats stats;
	private GameObject trailHolder;

	private const float UPDATEDISTANCE = 4f;

	private Vector3 lastPathPosition;
	private NavMeshPath navMeshPath;
	private Vector3[] corners;
	private LineRenderer connectingStrand;

    protected override void Awake()
    {
		base.Awake();
        stats = GetComponent<PlayerStats>();
		navMeshPath = new NavMeshPath();
		lastPathPosition = Vector3.one * 9999;
		trailHolder = transform.GetChild(3).gameObject;
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
				Debug.Log(1);
				UpdatePath(true);
				return;
			}
			else if(Input.GetKey(KeyCode.Space))
			{
				Debug.Log(2);
				UpdatePath(false);
				return;
			}
			else
			{
				Debug.Log(3);
				foreach(Transform child in trailHolder.transform)
				{
					Debug.Log("killing " + child.gameObject.name);
					Destroy(child.gameObject);
				}
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
			NavMesh.CalculatePath(trailPosition, transform.position, NavMesh.AllAreas, navMeshPath);
			corners = navMeshPath.corners;
			lastPathPosition = transform.position;
			foreach(Transform child in trailHolder.transform)
			{
				Destroy(child.gameObject);
			}
			for(int x = 0; x < corners.Length - 1; ++x)
			{
				LineRenderer temp = Instantiate(trailPrefab, Vector3.zero, Quaternion.identity, trailHolder.transform).GetComponent<LineRenderer>();
				temp.SetPositions(new Vector3[] { corners[x], corners[x + 1] });
				if(x == corners.Length - 2)
				{
					connectingStrand = temp;
				}
				
			}
		}
		else
		{
			if(connectingStrand != null)
			{
				connectingStrand.SetPosition(1, transform.position);
			}
		}
	}
}
