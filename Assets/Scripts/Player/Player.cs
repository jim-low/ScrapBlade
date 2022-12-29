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
	static SpawnCheckpoint spawnCheckpoint;

	void Start()
	{
		gameOverText.enabled = false;
		diedButtons.SetActive(false);
		source = GetComponent<AudioSource>();
		source.clip = deathSound;
		
		if (spawnCheckpoint == null)
		{
			spawnCheckpoint = GetComponent<SpawnCheckpoint>();
		}
	}

	public void UnDie()
	{
		isDied = false;
		gameOverText.enabled = isDied;
		diedButtons.SetActive(isDied);
	}

	public void Die()
	{
		if (isDied)
			return;

		isDied = true;
		source.Play();
		gameOverText.enabled = isDied;
		diedButtons.SetActive(isDied);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Checkpoint")
		{
			Debug.Log("hit checkpoint");
			SpawnCheckpoint.checkpoint = collider.gameObject.transform;
		}
	}
}
