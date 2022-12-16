using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordAttack : MonoBehaviour
{
    private int attackCount = 1;
    private int maxAttacks = 3;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (attackCount > maxAttacks)
            {
                attackCount = 1;
            }

            anim.SetTrigger("Attack" + attackCount);
            ++attackCount;
        }
    }
}
