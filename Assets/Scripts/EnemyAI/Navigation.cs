using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    private EnemyAI enemy;
    public NavMeshAgent agent;
    public static Transform target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (target)
        {
            //Debug.Log("Moving towards: " + target.name);
			agent.destination = target.position;
		}
        agent.speed = enemy.GetCurrentSpeed();
        agent.angularSpeed = 180f;
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }
}
