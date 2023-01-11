using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target) // if target has been found, set target position
        {
            agent.destination = target.position;
        }
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
