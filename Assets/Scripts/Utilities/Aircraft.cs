using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : Interactable
{
    void Start()
    {
        hint = "Drive Plane";
    }

    protected override void Action()
    {
        addPrefix = false;
        hint = "Sorry you cant drive the plane in this version of the game ;)";
    }
}
