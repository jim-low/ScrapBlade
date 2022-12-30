using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public string hint;
	public bool addPrefix = true;

	public void Interact()
	{
		Action();
	}

	protected virtual void Action()
	{
		//Debug.Log("Action is not set");
	}
}
