using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    private EnemyAI enemy;
    public static NavMeshAgent agent;
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
			agent.destination = target.position;
		}
        agent.speed = enemy.GetCurrentSpeed();
    }
}
