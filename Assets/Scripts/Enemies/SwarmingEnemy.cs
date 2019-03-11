using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmingEnemy : BaseEnemy
{
	public float preSwarmSpeed;
	public float swarmSpeed;
	public bool swarmCreated;

	public List<SwarmingEnemy> partOfSwarm;

	private const int SWARMAMOUNT = 4;
	private const int SWARMRADIUS = 3;

	private static SwarmingEnemy swarmLeader;

	private bool waiting;
	private Vector3 spawnPoint;

    protected override void Start()
    {
        base.Start();

		swarmCreated = false;
		if(swarmLeader == null)
		{
			swarmLeader = this;
		}
		GetComponent<NavMeshAgent>().speed = preSwarmSpeed;
		waiting = false;
		spawnPoint = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % framesBetweenUpdates != 0)
            return;
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
					Debug.Log("setting speed");
					GetComponent<NavMeshAgent>().speed = swarmSpeed;
				}
			}
			else
			{
				swarmLeader = this;
				movement.Move(spawnPoint);
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
					GetComponent<NavMeshAgent>().speed = swarmSpeed;
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
