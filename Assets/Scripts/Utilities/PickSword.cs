using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSword : Interactable
{
	private bool hasTakenSword = false;
	public Transform spawnPoint;
	public GameObject swordPrefab;

	void Start()
	{
		hint = "Take Sword";
	}

	protected override void Action()
	{
		if (hasTakenSword)
			return;

		Destroy(GameObject.Find("SwordRotationPivot"));

		GameObject swordInstance = Instantiate(swordPrefab, spawnPoint);
		swordInstance.name = "PlayerSword";
		hasTakenSword = true;
		SwordAttack.isPickedUp = true;
		hint = "";
	}
}
