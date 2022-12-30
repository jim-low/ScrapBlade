using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    private string playerTag;

    void Start()
    {
        playerTag = "Player";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == playerTag) // if player collided with kill floor, die
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}
