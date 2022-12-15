using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum State
{
	CHASE,
	PATROL,
	ATTACK,
}

public class EnemyAI : MonoBehaviour
{
	public Transform player;
	private State state;
	public float damping;
	public float walkSpeed;
	public float runSpeed;
	private float speed;
	private bool isIdle;
	private bool playerInSight;
	public float stoppingDistance;
	public float sightRange;
	public float fovAngle;
	public LayerMask playerLayer;
	private float idleTime;
	public float damage;
	public float attackTime;
	public float attackRange;
	private bool playerInAttack;
	private bool canAttack;
	private NavMeshAgent agent;

	public Transform[] waypoints;
	private int waypointIndex;

	void Awake()
	{
		state = State.PATROL;
		damping = 10f;
		walkSpeed = 5f;
		runSpeed = 8f;
		speed = walkSpeed;

		isIdle = false;
		stoppingDistance = 3f;
		attackRange = stoppingDistance;

		sightRange = 18f;
		fovAngle = 90f;

		idleTime = 5f;
		damage = 45f;
		attackTime = 1f;

		waypointIndex = 0;
		canAttack = true;
	}

	void Start()
	{
		if (waypoints.Length == 0)
		{
			Idle();
		}

		agent = GetComponent<Navigation>().GetAgent();
	}
	void Update()
	{
		DetectPlayerInSight();
		DetectPlayerInAttack();

		if (!isIdle && state == State.PATROL)
		{
			Patrol();
		}

		if (playerInSight)
		{
			LookAtPlayer();
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

	void LookAtPlayer()
	{
		Vector3 lookPos = player.position - transform.position;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}

	void DetectPlayerInSight()
	{
		if (state == State.PATROL) // if patrolling, depend on FOV angle to detect player
		{
			playerInSight = DetectPlayerInFOV();
		}

		if (state == State.CHASE) // if chasing, check if player is nearby and if is in FOV
		{
			if (Physics.CheckSphere(transform.position, sightRange, playerLayer)) // check if player is nearby
			{
				playerInSight = DetectPlayerInFOV();
			}
		}
	}

	bool DetectPlayerInFOV()
	{
		Vector3 direction = player.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);

		RaycastHit hit;
		bool hitTarget = Physics.Raycast(transform.position, direction, out hit, sightRange);
		if (hitTarget && hit.collider.gameObject.tag == "Player")
		{
			float distance = Vector3.Distance(player.position, transform.position);
			return (angle < fovAngle / 2) && distance <= sightRange;
		}
		return false;
	}

	void DetectPlayerInAttack()
	{
		playerInAttack = Physics.CheckSphere(transform.position, attackRange, playerLayer);
	}

	// States
	void Patrol()
	{
		state = State.PATROL;
		// patrol area
		speed = walkSpeed;
		Navigation.target = waypoints[waypointIndex];
		agent.stoppingDistance = 0f;
		agent.autoBraking = false;
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
		state = State.CHASE;
		StopCoroutine(Idle());
		speed = runSpeed;
		isIdle = false;
		Navigation.target = player;
		agent.stoppingDistance = stoppingDistance;
	}

	void Attack()
	{
		state = State.ATTACK;
		if (canAttack)
		{
			// TODO: let player be unalive here
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
		if (state == State.PATROL)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * sightRange);
		}

		if (state == State.CHASE && playerInSight)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, sightRange);
		}

		if (state == State.CHASE && !playerInSight)
		{
			Gizmos.color = Color.grey;
			Gizmos.DrawWireSphere(transform.position, sightRange);
		}

		if (state == State.ATTACK)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, attackRange);
		}
	}
}
