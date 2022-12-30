using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]


public class KillEnemy : MonoBehaviour
{
    [Header("Death Particle")]
    public GameObject deathSpark;
    private string sparkName;
    private string swordTag;

    void Start()
    {
        sparkName = "DeathSpark";
        swordTag = "Sword";
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == swordTag && Sword.isAttacking)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<RangedEnemy>().SetLiveStatus(false);
            GameObject temp = Instantiate(deathSpark, transform.position, Quaternion.identity);
            temp.name = sparkName;
        }
    }
}
