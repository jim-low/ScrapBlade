using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
public class Boss : MonoBehaviour
{
    private Animator anim;
    private RangedEnemy rangedBehavior;
    private bool hasPlayedWin = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rangedBehavior = GetComponent<RangedEnemy>();
        rangedBehavior.SetCanShoot(false);
    }

	void Update()
	{
		Attack();
	}

	void Attack()
	{
		if (anim.GetBool("Win"))
		{
			if (!isPlayingWin())
			{
				rangedBehavior.SetCanShoot(true);
			}
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
}
