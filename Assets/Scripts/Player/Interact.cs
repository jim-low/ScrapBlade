using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
	public Text hintText;
	public Camera cam;
	public LayerMask interactableLayer;
	private bool canInteract;

	RaycastHit hit;

	void Update()
	{
		DetectPlayerLineOfSight();

		if (canInteract && Input.GetKeyDown(KeyCode.E))
		{
            hit.collider.GetComponent<Interactable>().Interact();
		}
	}

	void DetectPlayerLineOfSight()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 3f, interactableLayer))
		{
			hintText.text = hit.collider.GetComponent<Interactable>().hint;
			hintText.enabled = true;
			canInteract = true;
		}
		else
		{
			hintText.text = "";
			hintText.enabled = false;
			canInteract = false;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(transform.position, cam.ScreenPointToRay(Input.mousePosition).direction * 5f);
	}
}
