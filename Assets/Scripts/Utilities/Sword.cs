using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Sword : MonoBehaviour
{
    private string[] attackNames = {
        "TopLeftBottomRightSlash",
        "TopRightBottomLeftSlash",
        "HorizontalSlash",
    };
	private int attackIndex = 0;
	private int maxAttacks = 2;
	private float blockDuration = 0.25f;
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
		// create 2 animations for blocking bullets
		isBlocking = true;
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

			//Debug.Log("attack index: " + attackIndex);
			//Debug.Log("attack name: " + attackNames[attackIndex]);

			if (attackIndex >= maxAttacks + 1)
			{
				attackIndex = 0;
			}
		}
	}

	private bool isPlaying()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.length > stateInfo.normalizedTime;
	}

}
