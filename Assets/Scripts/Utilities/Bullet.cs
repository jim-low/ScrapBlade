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
		bool isDestroyed = false;

		if (collider.gameObject.tag == "BlockBullet")
		{
			if (Sword.isBlocking)
			{
				Destroy(gameObject);
				Debug.Log("Bullet got BLOCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCKED");

				isDestroyed = true;
			}
		}

		if (collider.gameObject.tag == "Player")
		{
			Destroy(gameObject);
			Debug.Log("Player is died");
			isDestroyed = true;
		}

		if (!isDestroyed)
			Destroy(gameObject);
	}
}
