using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSword : Interactable
{
	private bool hasTakenSword = false;
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

		GameObject player = GameObject.Find("Player");
		GameObject swordInstance = Instantiate(swordPrefab, player.transform);
        swordInstance.name = "haha_sword_go_brrrrrrrrrrr";
		swordInstance.transform.position = new Vector3(0.3f, -0.3f, 0.4f);
		swordInstance.transform.localRotation = Quaternion.Euler(0f, 0f, 45f);
        hasTakenSword = true;
	}
}
