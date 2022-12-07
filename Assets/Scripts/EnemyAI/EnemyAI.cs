using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
	public GameObject player;
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
	private float chaseTime = 3f;
	public float damage = 5f;
	public float attackTime = 1f;
	public float attackRange;
	private bool playerInAttack;
	private bool canAttack;

	public Transform[] waypoints;
	private int waypointIndex;

	void Awake()
	{
		speed = walkSpeed;
		isIdle = false;
		waypointIndex = 0;
		canAttack = true;
		stoppingDistance = 3f;
		attackRange = stoppingDistance;
	}

	void Update()
	{
		DetectPlayerInSight();
		DetectPlayerInAttack();

		if (!isIdle && !playerInSight)
		{
			Patrol();
		}

		if (playerInSight)
		{
			transform.LookAt(player.transform);
		}

		if (playerInSight && !playerInAttack)
		{
			Chase();
		}

		if (playerInSight && playerInAttack)
		{
			Attack();
		}
	}

	void DetectPlayerInSight()
	{
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);
		RaycastHit hit;
		bool hitTarget = Physics.Raycast(transform.position, direction, out hit, sightRange);
		if (hitTarget && hit.collider.gameObject.tag == "Player")
		{
			float distance = Vector3.Distance(player.transform.position, transform.position);
			playerInSight = (angle < fovAngle / 2) && distance <= sightRange;
		}
	}

	void DetectPlayerInAttack()
	{
		playerInAttack = Physics.CheckSphere(transform.position, attackRange, playerLayer);
	}

	// States
	void Patrol()
	{
		// patrol area
		speed = walkSpeed;
		Navigation.target = waypoints[waypointIndex];
		Navigation.agent.stoppingDistance = 0f;
		Navigation.agent.autoBraking = false;
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
		StopCoroutine(Idle());
		speed = runSpeed;
		isIdle = false;
		Navigation.target = player.transform;
		Navigation.agent.stoppingDistance = stoppingDistance;
		Navigation.agent.autoBraking = true;
	}

	void Attack()
	{
		if (canAttack)
		{
			player.GetComponent<PlayerScriptForTesting>().Damage(damage);
			StartCoroutine(NextAttack());
		}
	}

	private IEnumerator NextAttack()
	{
		canAttack = false;
		yield return new WaitForSeconds(attackTime);
		canAttack = true;
	}

	public float GetCurrentSpeed()
	{
		return speed;
	}

	private void OnDrawGizmos() // just to draw line of sight
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * sightRange);

		Gizmos.color = playerInSight ? Color.blue : Color.yellow;
		Gizmos.DrawWireSphere(transform.position, sightRange);

		Gizmos.color = playerInAttack ? Color.red : Color.gray;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
