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
		if (collider.gameObject.tag == "BlockBullet" && Sword.isBlocking)
		{
			Debug.Log("Bullet got BLOCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCKED");
			IndicatorManager.blockedTimes++;
		}
		else if (collider.gameObject.tag == "Player")
		{
			Debug.Log("Player is dieded");
			IndicatorManager.diedTimes++;
		}

		Destroy(gameObject);
	}
}
