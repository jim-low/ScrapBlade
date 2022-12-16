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

		GameObject sword = GameObject.Find("Sword_Cyber_Cyan");
		Destroy(sword);

		GameObject player = GameObject.Find("PlayerSwordSpawnPoint");
		GameObject swordInstance = Instantiate(swordPrefab, spawnPoint);
        swordInstance.name = "PlayerSword";
        hasTakenSword = true;
	}
}
