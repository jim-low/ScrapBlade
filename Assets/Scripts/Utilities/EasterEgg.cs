using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// best script ever written for best functionality
public class EasterEgg : Interactable
{
    public GameObject easterEggBoard;
    public GameObject easterEggStartBoard;

    protected override void Action()
    {
        easterEggStartBoard.GetComponent<Animator>().enabled = true;
        new WaitForSeconds(1f);
        easterEggBoard.GetComponent<Animator>().enabled = true ;
    }
}
