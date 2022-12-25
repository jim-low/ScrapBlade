using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
	private bool isOpen = false;
	private AudioSource source;
	public AudioClip doorOpen;
	public AudioClip doorClose;
	private Animator anim;

	void Start()
	{
		anim = transform.parent.GetComponent<Animator>();
		source = transform.parent.GetComponent<AudioSource>();
		hint = "Open Door";
	}

	protected override void Action()
	{
		if (isOpen)
			return;

		isOpen = true;
		anim.SetBool("character_nearby", true);
		source.clip = doorOpen;
		source.Play();
		hint = "";
		StartCoroutine(CloseDoor());
	}

	IEnumerator CloseDoor()
	{
		yield return new WaitForSeconds(2f);
		isOpen = false;
		anim.SetBool("character_nearby", false);
		source.clip = doorClose;
		source.Play();
		hint = "Open Door";
	}
}
