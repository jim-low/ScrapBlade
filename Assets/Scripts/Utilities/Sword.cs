using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
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

	void Start()
	{
		anim = GetComponent<Animator>();
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

		blockBulletCollider.enabled = isBlocking;
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

		isAttacking = true;
		anim.SetTrigger("Attack" + (attackIndex + 1));
		++attackIndex;
		StartCoroutine(StopAttack());
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
