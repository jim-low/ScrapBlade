using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
	[Header("Boss Movement")]
	private float speed;
	public float walkSpeed;
	public float runSpeed;

	[Header("Boss Mechanic")]
	public float runningDistance = 15f;
	public float minFollowDistance;
	public bool follow;
	private Vector3 moveDir;

	[Header("References")]
	public Transform player;
	RangedEnemy rangedBehavior;
	private bool engagedPlayer = false;
	private Animator anim;
	private PlayerMovement playerState;
	private bool shooting = false;
	private Boss boss;
	private Navigation navigation;
	private NavMeshAgent agent;

	// Start is called before the first frame update
	void Start()
	{
		follow = false;
		rangedBehavior = GetComponent<RangedEnemy>();
		speed = walkSpeed;
		anim = GetComponent<Animator>();
		navigation = GetComponent<Navigation>();
		agent = navigation.GetAgent();
		boss = GetComponent<Boss>();
		playerState = GetComponent<Boss>().player.GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (follow && !engagedPlayer)
		{
			navigation.SetTarget(player);
			engagedPlayer = true;
		}

		Movement();
		CheckPlayerState();
	}

	void Movement()
	{
		float distance = Vector3.Distance(transform.position, player.position);

		if (shooting || distance <= minFollowDistance)
		{
			anim.SetBool("Running", false);
			anim.SetBool("Idle", true);
			SetSpeed("stop");

			if (!shooting)
				boss.SetCanKick(true);
		}
		else if (distance > minFollowDistance)
		{
			anim.SetBool("Idle", false);

			if (distance >= minFollowDistance + runningDistance)
			{
				anim.SetBool("Running", true);
				SetSpeed("run");
			}
			else
			{
				anim.SetBool("Running", false);
				SetSpeed("walk");
			}
		}
	}

	void CheckPlayerState()
	{
		// shoot when player is wall running or climbing or jumping when wall run
		shooting = (playerState.state == PlayerMovement.MovementState.wallrunning ||
			playerState.state == PlayerMovement.MovementState.climbing ||
			playerState.state == PlayerMovement.MovementState.wallRunJumping);

		anim.SetBool("Win", shooting);
	}

	void FixedUpdate()
	{
		follow = !shooting || speed > 0;
	}

	public void SetSpeed(string status)
	{
		if (status == "walk")
			navigation.SetSpeed(walkSpeed);

		if (status == "run")
			navigation.SetSpeed(runSpeed);

		if (status == "stop")
			navigation.SetSpeed(0);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = follow ? Color.yellow : Color.black;
		Gizmos.DrawWireSphere(transform.position, minFollowDistance);
	}
}
