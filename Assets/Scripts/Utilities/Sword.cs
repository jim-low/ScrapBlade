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
	private float blockDuration = 0.5f;
	public static bool isAttacking = false;
	public static bool isPickedUp = false;
	public static bool isBlocking = false;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		isAttacking = isPlaying();

		if (Input.GetMouseButtonDown(0))
			Attack();

		if (Input.GetKeyDown(KeyCode.F))
			BlockBullet();
	}

	void BlockBullet()
	{
		if (isAttacking)
		{
			return;
		}

		if (blockIndex >= maxBlocks + 1)
		{
			blockIndex = 0;
		}

		// create 2 animations for blocking bullets
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
		if (isPickedUp && !isAttacking)
		{
			anim.SetTrigger("Attack" + (attackIndex + 1));
			isAttacking = isPlaying();
			++attackIndex;

			if (attackIndex >= maxAttacks + 1)
			{
				attackIndex = 0;
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!isAttacking)
		{
			return;
		}

		if (collider.gameObject.tag == "Boss")
		{
			collider.gameObject.GetComponent<Boss>().SetIsHit(true);
		}
	}

	private bool isPlaying()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.length > stateInfo.normalizedTime;
	}
}
