using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public static bool isKicking = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && isKicking)
        {
            Transform curr = collider.gameObject.transform;
            if (curr.GetComponent<Player>() == null)
                curr = curr.parent;
        
            curr.GetComponent<Player>().Die();
        }
    }
}
