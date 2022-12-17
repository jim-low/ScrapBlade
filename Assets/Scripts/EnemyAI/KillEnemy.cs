using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KillEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Sword")
        {
            Debug.Log(gameObject.name + " has been unalived");
            GetComponent<Animator>().enabled = false;
        }
    }
}