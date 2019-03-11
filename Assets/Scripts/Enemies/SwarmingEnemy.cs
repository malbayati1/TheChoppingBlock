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
		spawnPoint = transform.position;
    }

	void OnDisable()
	{
		if(this == swarmLeader)
		{
			Object[] otherWasps = GameObject.FindObjectsOfType(typeof(SwarmingEnemy));
			SwarmingEnemy se;
			foreach(Object o in otherWasps)
			{
				se = (SwarmingEnemy)o;
				if(se != this)
				{
					foreach(SwarmingEnemy temp in partOfSwarm)
					{
						if(temp != null)
						{
							se.partOfSwarm.Add(temp);
						}
					}
					swarmLeader = se;
					se.enabled = true;
					break;
				}
			}
		}	
	}
    
    // Update is called once per frame
    protected override void Update()
    {
		if(!swarmCreated)
		{
			if(swarmLeader != null)
			{
				movement.Move(swarmLeader.transform.position);
				CheckIfSwarmCreated();
			}
			else
			{
				if(this == swarmLeader)
				{
					movement.Move(spawnPoint);
				}
				else
				{
					swarmLeader = this;
				}
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
					se.gameObject.GetComponent<NavMeshAgent>().speed = swarmSpeed;
					se.enabled = true;
				}
				swarmCreated = true;
				GetComponent<NavMeshAgent>().speed = swarmSpeed;
			}
		}
		else
		{
			if((transform.position - swarmLeader.transform.position).magnitude <= SWARMRADIUS)
			{
				swarmLeader.partOfSwarm.Add(this);
				swarmCreated = swarmLeader.swarmCreated;
				if(!swarmCreated)
				{
					this.enabled = false;
				}
			}
		}
	}
}
