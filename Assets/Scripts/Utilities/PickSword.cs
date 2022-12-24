using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSword : Interactable
{
	private bool hasTakenSword = false;
	private Light spotlight;
	public Transform spawnPoint;
	public GameObject swordPrefab;

	void Start()
	{
		hint = "Take Sword";
		spotlight = transform.parent.GetComponentInChildren<Light>();
	}

	void Update()
	{
		if (hasTakenSword)
		{
			spotlight.intensity = spotlight.intensity - 0.5f;
			if (spotlight.intensity <= 0)
			{
				spotlight.enabled = false;
			}
		}
	}

	protected override void Action()
	{
		if (hasTakenSword)
			return;

		// Destroy pivot point
		transform.parent.Find("SwordRotationPivot").gameObject.SetActive(false);

		// instantiate sword in player hand
		GameObject swordInstance = Instantiate(swordPrefab, spawnPoint);
		swordInstance.name = "PlayerSword";

		// setting flags
		hasTakenSword = true;
		swordInstance.GetComponent<Sword>().SetIsPickedUp(true);
		hint = "";
	}
}
