using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Boss : MonoBehaviour
{
	[Header("References")]
	public GameObject player;
	private Animator anim;
	private RangedEnemy rangedBehavior;
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

	[Header("Boss Defeat")]
	public GameObject bossDefeatEscapeDoor;

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
			PlaySound(winSound); // play boss win audio
			anim.SetBool(winBool, true); // play boss win animation
			rangedBehavior.SetCanShoot(false); // stop shooting
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

		BossAttack.isKicking = isPlayingKick(); // check if boss is kicking by checking if kick animation is playing
		ShootAttack();
	}

	private void PlaySound(AudioClip clip)
	{
		source.clip = clip;
		source.Play();
	}

	void Die()
	{
		PlaySound(deathSound); // play death voice line

		// stop navigating
		Navigation navigation = GetComponent<Navigation>();
		navigation.agent.enabled = false;
		navigation.enabled = false;

		// disable physics to make dead body untouchable
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<CapsuleCollider>().enabled = false;
		
		// disable shooting
		GetComponent<RangedEnemy>().enabled = false;

		// play death animation
		anim.SetBool(KOBool, true);
		died = true;

		// open escape door
		bossDefeatEscapeDoor.GetComponent<BossDefeatEscapeDoor>().OpenEscapeDoor();
	}

	void ShootAttack()
	{
		// if shooting animation is playing and is not kicking, begin shooting player
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