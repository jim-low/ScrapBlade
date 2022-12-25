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

	void Start()
	{
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
		maxSwooshes = swooshes.Length;
		blockBulletCollider = GameObject.Find("Player").transform.Find("BlockBulletCollider").GetComponent<BoxCollider>(); // what the fuck??
	}

	void Update()
	{
		if (!isPickedUp || Player.isDied)
			return;

		if (!isAttacking && Input.GetMouseButtonDown(0))
			Attack();

		if (!isBlocking && Input.GetMouseButtonDown(1))
			BlockBullet();

		blockBulletCollider.enabled = true;
	}

	void BlockBullet()
	{
		if (blockIndex >= maxBlocks + 1)
		{
			blockIndex = 0;
		}

		isBlocking = true;
		anim.SetTrigger("Block" + (blockIndex + 1));
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
		Debug.Log("Swoosh Index: " + swooshIndex);

		isAttacking = true;
		anim.SetTrigger("Attack" + (attackIndex + 1));
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

		if (collider.gameObject.tag == "Boss")
		{
			collider.gameObject.GetComponent<Boss>().Damaged();
			Debug.Log("boss has been hit");
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
