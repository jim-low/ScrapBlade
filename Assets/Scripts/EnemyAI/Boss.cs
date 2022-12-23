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
	private bool canKick = false;
	private int maxHitTimes = 5;
	private int hitTimes = 0;
	private bool isHit = false;

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
		if (Player.isDied)
		{
			anim.SetBool("Win", true);
			rangedBehavior.SetCanShoot(false);
			return;
		}

		if (hitTimes >= maxHitTimes)
		{
			Die();
			return;
		}

		if (!Player.isDied && canKick)
		{
			Kick();
		}

		ShootAttack();
	}

	void Die()
	{
		anim.SetBool("KO", true);
	}

	void ShootAttack()
	{
		Debug.Log("ShootAttack() is playing now!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		if (anim.GetBool("Win"))
		{
			if (!isPlayingWin())
			{
				rangedBehavior.SetCanShoot(true);
				Debug.Log("set to can shoot ady");
			}
		}
		else
		{
			rangedBehavior.SetCanShoot(false);
		}
	}

	private bool isPlayingWin()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.length > stateInfo.normalizedTime;
	}

	public void SetCanKick(bool canKickMou)
	{
		canKick = canKickMou;
	}

	void Kick()
	{
		anim.SetTrigger("Hit1");
	}

	public void SetIsHit(bool hasBeenHit)
	{
		anim.SetTrigger("Damage");
		++hitTimes;
		isHit = hasBeenHit;
	}

	void Damaged()
	{
		isHit = false;
	}
}