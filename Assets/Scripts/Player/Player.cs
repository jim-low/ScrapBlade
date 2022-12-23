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
			gameOverText.enabled = true;
		}
	}
}
