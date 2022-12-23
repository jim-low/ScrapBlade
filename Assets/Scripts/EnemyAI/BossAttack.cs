using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public static bool isKicking = false;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Boss is kicking: " + isKicking);
        if (collider.gameObject.tag == "Player" && isKicking)
        {
            Player.isDied = true;
            Debug.Log("Boss has kicked the player");
        }
    }
}
