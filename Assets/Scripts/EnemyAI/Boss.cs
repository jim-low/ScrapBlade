using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
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
	private bool midHealthReached = false;
	private bool lastHealthReached = false;

	[Header("Audio")]
	private AudioSource source;
	public AudioClip damagedSound;
	public AudioClip deathSound;
	public AudioClip intro;
	public AudioClip kickSound;
	public AudioClip winSound;
	public AudioClip lastHealth;
	public AudioClip midHealth;
	//strings to use
	private string winBool;
	private string KOBool;
	private string hitName;
	private string hitTrigger;
	private string dmgTrigger;

	void Start()
	{
		winBool = "Win";
		KOBool = "KO";
        hitName = "hit01";
		hitTrigger = "Hit1";
		dmgTrigger = "Damage";

        anim = GetComponent<Animator>();
		rangedBehavior = GetComponent<RangedEnemy>();
		rangedBehavior.SetCanShoot(false);
		playerState = player.GetComponent<PlayerMovement>();
		bossMovement = GetComponent<BossMovement>();
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (died)
		{
			return;
		}

		if (Player.isDied)
		{
			PlaySound(winSound);
			anim.SetBool(winBool, true);
			rangedBehavior.SetCanShoot(false);
			return;
		}

		if (hitTimes >= maxHitTimes)
		{
			Die();
			return;
		}

		if (!lastHealthReached && hitTimes >= maxHitTimes - 1)
		{
			lastHealthReached = true;
			PlaySound(midHealth);
		}

		if (!midHealthReached && hitTimes >= maxHitTimes / 2)
		{
			midHealthReached = true;
			PlaySound(midHealth);
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

	private void PlaySound(AudioClip clip)
	{
		source.clip = clip;
		source.Play();
	}

	void Die()
	{
		PlaySound(deathSound);
		Navigation navigation = GetComponent<Navigation>();
		navigation.agent.enabled = false;
		navigation.enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<CapsuleCollider>().enabled = false;
		GetComponent<RangedEnemy>().enabled = false;
		anim.SetBool(KOBool, true);
		died = true;
	}

	void ShootAttack()
	{
		if (anim.GetBool(winBool))
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
		return stateInfo.IsName(hitName) && stateInfo.normalizedTime < 1.0f;
	}

	public void SetCanKick(bool canKickMou)
	{
		canKick = canKickMou;
	}

	void Kick()
	{
		PlaySound(kickSound);
		anim.SetTrigger(hitTrigger);
	}

	public void Damaged()
	{
		anim.SetTrigger(dmgTrigger);
		++hitTimes;
	}
}