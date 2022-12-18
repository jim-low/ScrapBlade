using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KillEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Sword" && SwordAttack.isAttacking)
        {
            GetComponent<Animator>().enabled = false;
        }
    }
}
