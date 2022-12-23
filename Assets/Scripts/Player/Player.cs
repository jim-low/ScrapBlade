using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public static bool isDied = false;
	public Text gameOverText;
	public GameObject diedButtons;

	void Start()
	{
		gameOverText.enabled = false;
		diedButtons.SetActive(false);
	}

	void Update()
	{
		if (isDied)
		{
			gameOverText.enabled = true;
			diedButtons.SetActive(true);
		}
	}
}
