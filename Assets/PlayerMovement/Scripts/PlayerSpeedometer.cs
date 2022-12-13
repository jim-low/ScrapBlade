using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedometer : MonoBehaviour
{
    public static string speedMsg;
    Text Speedometer;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Speedometer = GetComponent<Text>();
        Speedometer.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Speedometer.text = speedMsg;
    }
}
