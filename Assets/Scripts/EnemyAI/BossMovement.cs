using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	Rigidbody rb;
	RangedEnemy rangedBehavior;
	private bool engagedPlayer = false;
	private Animator anim;
	private PlayerMovement playerState;
	private bool shooting = false;

	// Start is called before the first frame update
	void Start()
	{
		follow = false;
		rb = GetComponent<Rigidbody>();
		rangedBehavior = GetComponent<RangedEnemy>();
		speed = walkSpeed;
		anim = GetComponent<Animator>();

		Navigation.agent.stoppingDistance = minFollowDistance;
		Navigation.agent.autoBraking = true;
        playerState = GetComponent<Boss>().player.GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (follow && !engagedPlayer)
		{
			Navigation.target = player;
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
		}
		else if (distance > minFollowDistance)
		{
			anim.SetBool("Idle", false);

			if (distance >= runningDistance)
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
			Navigation.agent.speed = walkSpeed;

		if (status == "run")
			Navigation.agent.speed = runSpeed;

		if (status == "stop")
			Navigation.agent.speed = 0;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = follow ? Color.yellow : Color.black;
		Gizmos.DrawWireSphere(transform.position, minFollowDistance);
	}
}
