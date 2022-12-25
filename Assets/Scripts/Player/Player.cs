using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
	public static bool isDied = false;
	public Text gameOverText;
	public GameObject diedButtons;
	private AudioSource source;
	public AudioClip deathSound;

	void Start()
	{
		gameOverText.enabled = false;
		diedButtons.SetActive(false);
		source = GetComponent<AudioSource>();
		source.clip = deathSound;
	}

	public void Die()
	{
		if (isDied)
			return;

		isDied = true;
		source.Play();
		gameOverText.enabled = isDied;
		diedButtons.SetActive(isDied);
		Cursor.lockState = isDied ? CursorLockMode.None : CursorLockMode.Locked;
	}
}
