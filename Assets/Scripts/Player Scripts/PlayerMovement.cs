using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    private PlayerStats stats;

    protected override void Awake()
    {
		base.Awake();
        stats = GetComponent<PlayerStats>();
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
    }

    public override void Move(float xInput, float zInput)
    {
        //xInput *= stats.movementSpeed.value;
        //zInput *= stats.movementSpeed.value;
        base.Move(xInput, zInput);
    }

	void SetMoveSpeed(float speed)
	{
		navMeshAgent.speed = speed;
	}
}
