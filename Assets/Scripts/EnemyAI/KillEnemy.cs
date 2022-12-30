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
        if (collider.gameObject.tag == swordTag && Sword.isAttacking) // only activate if sword is attacking (when animation is playing)
        {
            GetComponent<Animator>().enabled = false; // disable enemy animator to allow ragdoll effect
            GetComponent<RangedEnemy>().SetLiveStatus(false); // unalive enemy

            // play spark animation
            GameObject temp = Instantiate(deathSpark, transform.position, Quaternion.identity);
            temp.name = sparkName;
        }
    }
}
