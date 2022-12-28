using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
			Transform currGO = GetComponent<Collider>().gameObject.transform;
			while (currGO.GetComponent<Player>() == null)
			{
				currGO = currGO.transform.parent;
			}
			currGO.GetComponent<Player>().Die();
		}
	}
}
