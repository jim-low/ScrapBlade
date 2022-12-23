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
	private bool canKick = false;
	private int maxHitTimes = 5;
	private int hitTimes = 0;

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

		ShootAttack();
		CheckHealth();
		if (!Player.isDied && canKick)
			Kick();
	}

	void CheckHealth()
	{
		if (hitTimes >= maxHitTimes)
		{
			Debug.Log("Boss is dieded this time");
		}
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

	public void SetCanKick(bool canKickMou)
	{
		canKick = canKickMou;
	}

	void Kick()
	{
		anim.SetTrigger("Hit1");
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Sword")
		{
			++hitTimes;
			Debug.Log("boss is hit! " + (maxHitTimes - hitTimes) + " more hits left");
		}
	}
}