using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public static bool isKicking = false;
    private string playerTag;

    void Start()
    {
        playerTag = "Player";
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == playerTag && isKicking)
        {
            Transform curr = collider.gameObject.transform;
            if (curr.GetComponent<Player>() == null) // go up in hierarchy to get player game object
                curr = curr.parent;

            curr.GetComponent<Player>().Die();
        }
    }
}
