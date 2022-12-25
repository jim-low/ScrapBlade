using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
	private Rigidbody rb;
	public float speed = 2f;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		rb.AddRelativeForce(transform.forward * speed, ForceMode.Force);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "BlockBullet")
		{
			Destroy(gameObject);
		}
		else if (collider.gameObject.tag == "Player")
		{
			Transform currGO = collider.gameObject.transform;
			while (currGO.GetComponent<Player>() == null)
			{
				currGO = currGO.transform.parent;
			}
			currGO.GetComponent<Player>().Die();

			Destroy(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

	}
}
