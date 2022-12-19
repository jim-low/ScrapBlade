using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
[RequireComponent(typeof(Rigidbody))]
public class Boss : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    private Rigidbody rb;
    private RangedEnemy rangedBehavior;
    private bool hasPlayedWin = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rangedBehavior = GetComponent<RangedEnemy>();
        rb = GetComponent<Rigidbody>();
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
