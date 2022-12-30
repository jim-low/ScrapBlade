using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : Interactable
{
	private bool isOpen = false;
	private AudioSource source;
	public AudioClip doorOpen;
	public AudioClip doorClose;
	private Animator anim;
	private string charNearBool;
	private string emptyStr;
	private string openDoor;

	void Start()
	{
		charNearBool = "character_nearby";
		emptyStr = "";
		openDoor = "Open Door";
		anim = transform.parent.GetComponent<Animator>();
		source = GetComponent<AudioSource>();
		hint = openDoor;                                //initialize the hint
	}

	protected override void Action()
	{
		if (isOpen)
			return;

		isOpen = true;
		anim.SetBool(charNearBool, true);
		source.clip = doorOpen;
		source.Play();
		hint = emptyStr;
		StartCoroutine(CloseDoor());
	}

	IEnumerator CloseDoor()
	{
		yield return new WaitForSeconds(2f);
		isOpen = false;
		anim.SetBool(charNearBool, false);
		source.clip = doorClose;
		source.Play();
		hint = openDoor;
	}
}
