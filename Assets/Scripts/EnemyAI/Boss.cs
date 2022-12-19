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
    private bool followPlayer = true;
    public float walkSpeed = 2f;
    public float runSpeed = 7f;
    private float speed;

    void Start()
    {
        anim = GetComponent<Animator>();
        rangedBehavior = GetComponent<RangedEnemy>();
        rb = GetComponent<Rigidbody>();
        rangedBehavior.SetCanShoot(false);
        speed = walkSpeed;
    }

	void Update()
	{
		Attack();

        if (Vector3.Distance(transform.position, player.position) < 3f)
        {
            followPlayer = false;
        }

        FollowPlayer();
	}

    void FollowPlayer()
    {
        if (!followPlayer)
            return;

		anim.SetBool("Idle", false);
		anim.SetBool("Running", true);
		speed = walkSpeed;
		rb.AddForce(transform.forward * speed, ForceMode.Impulse);
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
