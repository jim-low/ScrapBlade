using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorManager : MonoBehaviour
{
    public Text diedText;
    public Text blockedText;

    public static int diedTimes = 0;
    public static int blockedTimes = 0;

    void Update()
    {
        diedText.text = "Died: " + diedTimes;
        blockedText.text = "Blocked: " + blockedTimes;
    }
}
