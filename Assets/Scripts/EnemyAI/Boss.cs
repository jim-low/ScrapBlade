using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
[RequireComponent(typeof(Rigidbody))]
public class Boss : MonoBehaviour
{
	public GameObject player;
	private Animator anim;
	private RangedEnemy rangedBehavior;
	private bool hasPlayedWin = false;
	private PlayerMovement playerState;
	private BossMovement bossMovement;
	private bool canKick = false;
	private int maxHitTimes = 5;
	private int hitTimes = 0;
	private bool died = false;
	private bool isHit = false;

	void Start()
	{
		anim = GetComponent<Animator>();
		rangedBehavior = GetComponent<RangedEnemy>();
		rangedBehavior.SetCanShoot(false);
		playerState = player.GetComponent<PlayerMovement>();
		bossMovement = GetComponent<BossMovement>();
	}

	void Update()
	{
		if (died)
		{
			return;
		}

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
			canKick = false;
		}

		BossAttack.isKicking = isPlayingKick(); // this does not work
		//Debug.Log("Boss is kicking: " + BossAttack.isKicking);
		ShootAttack();
	}

	void Die()
	{
		Navigation navigation = GetComponent<Navigation>();
		navigation.agent.enabled = false;
		navigation.enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<CapsuleCollider>().enabled = false;
		GetComponent<RangedEnemy>().enabled = false;
		anim.SetBool("KO", true);
		died = true;
	}

	void ShootAttack()
	{
		if (anim.GetBool("Win"))
		{
			if (!isPlayingKick())
			{
				rangedBehavior.SetCanShoot(true);
			}
		}
		else
		{
			rangedBehavior.SetCanShoot(false);
		}
	}

	private bool isPlayingKick()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.IsName("hit01") && stateInfo.normalizedTime < 1.0f;
	}

	public void SetCanKick(bool canKickMou)
	{
		canKick = canKickMou;
	}

	void Kick()
	{
		anim.SetTrigger("Hit1");
	}

	public void Damaged()
	{
		anim.SetTrigger("Damage");
		++hitTimes;
	}
}