using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KillEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Sword" && Sword.isAttacking)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<RangedEnemy>().SetLiveStatus(false);
        }
    }
}
