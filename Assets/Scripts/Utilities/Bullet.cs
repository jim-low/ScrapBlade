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
		bool bulletBlocked = false;
		if (collider.gameObject.tag == "BlockBullet")
		{
			bulletBlocked = true;
			Debug.Log("bullet got BLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOCKED");
		}
		else if (collider.gameObject.tag == "Player" && !bulletBlocked)
		{
			//Player.isDied = true;
			Debug.Log("Player is dieded");
		}

		Destroy(gameObject);
	}
}
