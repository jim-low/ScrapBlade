using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool isOpen = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        hint = "Open Door";
    }

	protected override void Action()
	{
		if (isOpen)
			return;

		isOpen = true;
		anim.SetBool("character_nearby", true);
		hint = "";
		StartCoroutine(CloseDoor());
	}

	IEnumerator CloseDoor()
	{
		yield return new WaitForSeconds(2f);
		isOpen = false;
		anim.SetBool("character_nearby", false);
		hint = "Open Door";
	}
}
