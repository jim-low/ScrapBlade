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
			Transform currGO = collider.gameObject.transform;
			while (currGO.GetComponent<Player>() == null)
			{
				currGO = currGO.transform.parent;
			}
            currGO.GetComponent<Player>().Die();
        }
    }
}
