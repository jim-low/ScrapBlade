using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static bool isDied = false;
    public Text gameOverText;

	void Start()
	{
        gameOverText.enabled = false;
	}

	void Update()
	{
		if (isDied)
		{
			GetComponent<PlayerMovement>().enabled = false;
			if (Sword.isPickedUp)
			{
				transform.Find("CameraHolder").Find("PlayerCam").Find("SwordSpawnPoint").Find("PlayerSword").GetComponent<Sword>().enabled = false;
			}
			gameOverText.enabled = true;
		}
	}
}
