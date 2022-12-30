using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Sword : MonoBehaviour
{
	private int attackIndex = 0;
	private int maxAttacks = 2;
	private int blockIndex = 0;
	private int maxBlocks = 1;
	public bool isPickedUp = false;
	public static bool isAttacking = false;
	private float attackDuration = 0.9f;
	public static bool isBlocking = false;
	private float blockDuration = 0.25f;
	BoxCollider blockBulletCollider;
	private Animator anim;
	private AudioSource source;
	public AudioClip[] swooshes;
	private int swooshIndex = 0;
	private int maxSwooshes = 2;
	private float swooshDelay = 0.2f;

	// string references
	private string playerObject;
	private string blockBulletColliderName;
	private string block;
	private string attack;
	private string bossTag;

	void Start()
	{
		playerObject = "Player";
		blockBulletColliderName = "BlockBulletCollider";
		block = "Block";
		attack = "Attack";
		bossTag = "Boss";
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
		maxSwooshes = swooshes.Length;
		blockBulletCollider = GameObject.Find(playerObject).transform.Find(blockBulletColliderName).GetComponent<BoxCollider>();
	}

	void Update()
	{
		if (!isPickedUp || Player.isDied)
			return;

		if (!isAttacking && Input.GetMouseButtonDown(0))
			Attack();

		if (!isBlocking && Input.GetMouseButtonDown(1))
			BlockBullet();

		blockBulletCollider.enabled = isBlocking;
	}

	void BlockBullet()
	{
		if (blockIndex >= maxBlocks + 1)
		{
			blockIndex = 0;
		}

		isBlocking = true;
		anim.SetTrigger(block + (blockIndex + 1));
		++blockIndex;
		StartCoroutine(StopBlock());
	}

	IEnumerator StopBlock()
	{
		yield return new WaitForSeconds(blockDuration);
		isBlocking = false;
	}

	void Attack()
	{
		if (attackIndex >= maxAttacks + 1)
		{
			attackIndex = 0;
		}

		if (swooshIndex > maxSwooshes - 1)
		{
			swooshIndex = 0;
		}

		isAttacking = true;
		anim.SetTrigger(attack + (attackIndex + 1));
		StartCoroutine(SwordAttackSoundBecauseItSoundsCool());
		++attackIndex;
		StartCoroutine(StopAttack());
	}

	IEnumerator SwordAttackSoundBecauseItSoundsCool()
	{
		yield return new WaitForSeconds(swooshDelay);
		source.clip = swooshes[swooshIndex];
		source.Play();
		++swooshIndex;
	}

	IEnumerator StopAttack()
	{
		yield return new WaitForSeconds(attackDuration);
		isAttacking = false;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!isAttacking)
		{
			return;
		}

		if (collider.gameObject.tag == bossTag)
		{
			collider.gameObject.GetComponent<Boss>().Damaged();
		}
	}

	private bool isPlaying()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.length > stateInfo.normalizedTime;
	}

	public void SetIsPickedUp(bool picked)
	{
		isPickedUp = picked;
	}
}
