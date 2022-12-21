using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
[RequireComponent(typeof(Rigidbody))]
public class Boss : MonoBehaviour
{
	public GameObject player;
	private Animator anim;
	private Rigidbody rb;
	private RangedEnemy rangedBehavior;
	private bool hasPlayedWin = false;
	private PlayerMovement playerState;
	private BossMovement bossMovement;

	void Start()
	{
		anim = GetComponent<Animator>();
		rangedBehavior = GetComponent<RangedEnemy>();
		rb = GetComponent<Rigidbody>();
		rangedBehavior.SetCanShoot(false);
		playerState = player.GetComponent<PlayerMovement>();
		bossMovement = GetComponent<BossMovement>();
	}

	void Update()
	{
		if (playerState.state == PlayerMovement.MovementState.wallrunning)
		{
			anim.SetBool("Win", true);
			bossMovement.SetSpeed("stop");
		}
		else if (playerState.state != PlayerMovement.MovementState.sprinting)
		{
			anim.SetBool("Win", false);
			bossMovement.SetSpeed("walk");
		}

		ShootAttack();
	}

	void ShootAttack()
	{
		if (anim.GetBool("Win"))
		{
			if (!isPlayingWin())
				rangedBehavior.SetCanShoot(true);
		}
		else
		{
			rangedBehavior.SetCanShoot(false);
		}
	}

	IEnumerator PrepareShoot(float seconds, bool canShoot)
	{
		yield return new WaitForSeconds(seconds);
		rangedBehavior.SetCanShoot(canShoot);
	}

	private bool isPlayingWin()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.length > stateInfo.normalizedTime;
	}
}
