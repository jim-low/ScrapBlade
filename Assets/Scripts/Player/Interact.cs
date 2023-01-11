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

    // string references
    private string EKey;
    private string emptyStr;

    RaycastHit hit;

    void Start()
    {
        EKey = " (E)";
        emptyStr = "";
    }
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
            string text = hit.collider.GetComponent<Interactable>().hint; // get interact hint text
            bool addPrefx = hit.collider.GetComponent<Interactable>().addPrefix; // determine whether to add "(E)" prefix to interact string
            if (text.Length > 0)
            {
                hintText.text = text;
                if (addPrefx)
                    hintText.text = text + EKey;

                hintText.enabled = true;
                canInteract = true;
            }
        }
        else
        {
            hintText.text = emptyStr;
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
