using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool hasBeenUnalived = false;
    public Text dedText;

	void Start()
	{
        dedText.enabled = false;
	}

	public void UnAlive()
	{
		hasBeenUnalived = true;
		dedText.enabled = true;
	}
}
