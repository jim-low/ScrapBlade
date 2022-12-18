using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RangedEnemy))]
public class Boss : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private float recoilTime;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
