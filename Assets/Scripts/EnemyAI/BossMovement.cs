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
	public float walkDistance;
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

	private string runBool;
	private string idleBool;
	private string stopName;
	private string winBool;
	private string runName;
	private string walkName;

	// Start is called before the first frame update
	void Start()
	{
		runBool = "Running";
		idleBool = "Idle";
		stopName = "stop";
		winBool = "Win";
		runName = "run";
		walkName = "walk";
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
			anim.SetBool(runBool, false);
			anim.SetBool(idleBool, true);
			SetSpeed(stopName);

			if (!shooting)
				boss.SetCanKick(true);
		}
		else if (distance > minFollowDistance)
		{
			anim.SetBool(idleBool, false);

			if (distance >= minFollowDistance + walkDistance)
			{
				anim.SetBool(runBool, true);
				SetSpeed(runName);
			}
			else
			{
				anim.SetBool(runBool, false);
				SetSpeed(walkName);
			}
		}
	}

	void CheckPlayerState()
	{
		// shoot when player is wall running or climbing or jumping when wall run
		if (playerState.state == PlayerMovement.MovementState.wallrunning)
			shooting = true;
		else if (playerState.grounded)
			shooting = false;

		anim.SetBool(winBool, shooting);
	}

	void FixedUpdate()
	{
		follow = !shooting || speed > 0;
	}

	public void SetSpeed(string status)
	{
		if (status == walkName)
			navigation.SetSpeed(walkSpeed);

		if (status == runName)
			navigation.SetSpeed(runSpeed);

		if (status == stopName)
			navigation.SetSpeed(0);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = follow ? Color.yellow : Color.black;
		Gizmos.DrawWireSphere(transform.position, minFollowDistance);
	}
}
