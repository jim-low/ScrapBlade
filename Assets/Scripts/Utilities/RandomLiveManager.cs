using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomLiveManager : MonoBehaviour
{
    public Text diedText;
    public Text blockedText;
    public static int numberOfTimesDied = 0;
    public static int numberOfTimesBlockedBullet = 0;

    // Update is called once per frame
    void Update()
    {
        diedText.text = "Died: " + numberOfTimesDied;
        blockedText.text = "Blocked: " + numberOfTimesBlockedBullet;
    }
}
