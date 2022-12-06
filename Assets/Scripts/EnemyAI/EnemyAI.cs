using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
	private GameObject player;
	public float walkSpeed = 5f;
	public float runSpeed = 15f;
	private float speed;
	private bool isIdle;
	private bool playerInSight;
	public float stoppingDistance = 2f;
	public float sightRange = 18f;
	public float fovAngle = 90f;
	public LayerMask playerLayer;

	private float idleTime = 5f;

	public Transform[] waypoints;
	private int waypointIndex;

	void Awake()
	{
		speed = walkSpeed;
		isIdle = false;
		waypointIndex = 0;
	}

	void Start()
	{
		player = GameObject.Find("Player");
	}

	void Update()
	{
		Debug.Log(waypointIndex);
		DetectPlayer();

		if (!isIdle && !playerInSight)
		{
			Patrol();
		}

		if (playerInSight)
		{
			Chase();
		}
	}

	void DetectPlayer()
	{
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, direction, out hit, sightRange, playerLayer))
		{
			float distance = Vector3.Distance(player.transform.position, transform.position);
			playerInSight = (angle < fovAngle / 2) && distance <= sightRange;
		}
	}

	// States
	void Patrol()
	{
		Debug.Log("patroling");
		// patrol area
		speed = walkSpeed;
		Navigation.target = waypoints[waypointIndex];
		Navigation.agent.stoppingDistance = 0f;
	}

	private void OnTriggerEnter(Collider collided)
	{
		if (collided.tag == "Waypoint") // switch waypoint
		{
			StartCoroutine(Idle());
			++waypointIndex;
			if (waypointIndex >= waypoints.Length)
			{
				waypointIndex = 0;
			}
		}
	}

	IEnumerator Idle()
	{
		isIdle = true;
		yield return new WaitForSeconds(idleTime);
		isIdle = false;
	}

	void Chase()
	{
		Debug.Log("chasing");
		StopCoroutine(Idle());
		speed = runSpeed;
		isIdle = false;
		Navigation.target = player.transform;
		Navigation.agent.stoppingDistance = stoppingDistance;
		transform.LookAt(player.transform);
	}

	public float GetCurrentSpeed()
	{
		return speed;
	}

	private void OnDrawGizmos()
	{
		if (playerInSight)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, player.transform.position);
		}
	}
}
