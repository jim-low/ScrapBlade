using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    public static NavMeshAgent agent;
    public static Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
			agent.destination = target.position;
		}
	}

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
}
