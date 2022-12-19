using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
public class Boss : MonoBehaviour
{
    private Animator anim;
    private RangedEnemy rangedBehavior;

    void Start()
    {
        anim = GetComponent<Animator>();
        rangedBehavior = GetComponent<RangedEnemy>();
        rangedBehavior.SetCanShoot(false);
    }

	void Update()
	{
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("Win", true);
            StartCoroutine(PrepareShoot(1.5f, true));
        }
	}

    IEnumerator PrepareShoot(float seconds, bool status)
    {
        yield return new WaitForSeconds(seconds);
        rangedBehavior.SetCanShoot(status);

    }
}
