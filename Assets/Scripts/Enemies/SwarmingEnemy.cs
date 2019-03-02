using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmingEnemy : Unit
{
    public int damageModifier = 1;
    public int knockbackModifier = 1;
	public float preSwarmSpeed;
	public float swarmSpeed;
	public bool swarmCreated;

	public List<SwarmingEnemy> partOfSwarm;

	private const int SWARMAMOUNT = 5;
	private const int SWARMRADIUS = 3;

	private static SwarmingEnemy swarmLeader;

    private GameObject player;
	private bool waiting;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
		swarmCreated = false;
		if(swarmLeader == null)
		{
			swarmLeader = this;
		}
		GetComponent<NavMeshAgent>().speed = preSwarmSpeed;
		waiting = false;
    }
    
    // Update is called once per frame
    void Update()
    {
		if(!swarmCreated)
		{
			if(swarmLeader != null)
			{
				if(!waiting)
				{
					movement.Move(swarmLeader.transform.position);
				}
				else
				{
					movement.Move(transform.position);
				}
				CheckIfSwarmCreated();
				if(swarmCreated)
				{
					GetComponent<NavMeshAgent>().speed = swarmSpeed;
				}
			}
			else
			{
				swarmLeader = this;
			}
		}
		else
		{
        	movement.Move(player.transform.position);
		}
    }

	void CheckIfSwarmCreated()
	{
		if(swarmLeader == this)
		{
			if(partOfSwarm.Count >= SWARMAMOUNT)
			{
				foreach(SwarmingEnemy se in partOfSwarm)
				{
					se.swarmCreated = true;
				}
				swarmCreated = true;
			}
		}
		else
		{
			if(!waiting && (transform.position - swarmLeader.transform.position).magnitude <= SWARMRADIUS)
			{
				waiting = true;
				swarmLeader.partOfSwarm.Add(this);
				swarmCreated = swarmLeader.swarmCreated;
			}
		}
	}
}
