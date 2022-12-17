using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordAttack : MonoBehaviour
{
    private string[] attackNames = {
        "TopLeftBottomRightSlash",
        "TopRightBottomLeftSlash",
        "HorizontalSlash",
    };
	private int attackIndex = 0;
	private int maxAttacks = 2;
	public static bool isAttacking = false;
	public static bool isPickedUp = false;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (isPickedUp && !isAttacking && Input.GetMouseButtonDown(0))
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
		else
		{
			isAttacking = isPlaying();
		}

		Debug.Log("isAttacking: " + isAttacking);
	}

	private bool isPlaying()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.length > stateInfo.normalizedTime;
	}

}
