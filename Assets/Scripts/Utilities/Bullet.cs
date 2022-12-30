using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
	private Rigidbody rb;
	public float speed = 2f;
	private string bulletTag;
	private string playerTag;

	void Start()
	{
		bulletTag = "BlockBullet";
		playerTag = "Player";
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		rb.AddRelativeForce(transform.forward * speed, ForceMode.Force);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == bulletTag)
		{
			Destroy(gameObject);
		}
		else if (collider.gameObject.tag == playerTag)
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
