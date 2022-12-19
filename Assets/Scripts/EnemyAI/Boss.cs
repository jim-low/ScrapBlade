using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
[RequireComponent(typeof(Rigidbody))]
public class Boss : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    private Rigidbody rb;
    private RangedEnemy rangedBehavior;
    private bool hasPlayedWin = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        anim = GetComponent<Animator>();
        rangedBehavior = GetComponent<RangedEnemy>();
        rb = GetComponent<Rigidbody>();
        rangedBehavior.SetCanShoot(false);
	playerMovement = player.GetComponent<PlayerMovement>();
    }

	void Update()
	{
		ShootAttack();
	}

	void ShootAttack()
	{
		if (anim.GetBool("Win")) {
			if (!isPlayingWin())
			{
				Debug.Log("yay is correct");
				rangedBehavior.SetCanShoot(true);
			}
		}
		else
		{
			Debug.Log("something is wrong hmmmmmmmmmmm");
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
