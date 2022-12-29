using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTimer : MonoBehaviour
{
    //script must be placed in the text timer to persist throughout scenes 

    static float deltaSeconds = 0;
    static int secondsPassed = 0;
    static int minutesPassed = 0;
    static string finalTime;
    static string onCountTime;
    static int highestTime = 0;

    //string variables
    string substituteZero = "0";
    string colon = ":";
    string minutes = "minutes ";
    string seconds = "seconds ";

    //indication of the end of game
    static bool gameEnd = false;

    //object grabbing
    GameObject timeText;

    // Start is called before the first frame update
    void Start()
    {
        //get the text object
        timeText = GameObject.Find("PlayerUI").transform.Find("Timer").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //check if 60 seconds have passed.
        if (secondsPassed >= 60)
        {
            secondsPassed -= 60; //remove count
        }

        //if the game ends, stop counting the timer, display in the credits scene
        if (gameEnd)
        {
            //check if the time is faster, overwrite if it is
            if ((secondsPassed + (minutesPassed * 60)) < highestTime) {
                highestTime = (secondsPassed + (minutesPassed * 60));
                //format and convert to string
                finalTime = minutesPassed.ToString() + minutes + secondsPassed.ToString() + seconds;
            }
        }

        else 
        { 
            //count and convert to seconds and minutes passed. 
            deltaSeconds += Time.deltaTime;
            secondsPassed = Mathf.FloorToInt(deltaSeconds % 60); //secondsPassed will update in seconds. 
            minutesPassed = Mathf.FloorToInt(deltaSeconds / 60); //minutesPassed will update in minutes. 
        }

        //format to string and display the time.
        if (secondsPassed < 10)
        {
            if (minutesPassed < 10)
            {
                onCountTime = substituteZero + minutesPassed.ToString() + colon + substituteZero + secondsPassed.ToString();
            }

            else
            {
                onCountTime = minutesPassed.ToString() + colon + substituteZero + secondsPassed.ToString();
            }
        }

        else {
            if (minutesPassed < 10)
            {
                onCountTime = substituteZero + minutesPassed.ToString() + colon + secondsPassed.ToString();
            }

            else
            {
                onCountTime = minutesPassed.ToString() + colon + secondsPassed.ToString();
            }

        }

        // display the time
        timeText.GetComponent<UnityEngine.UI.Text>().text = onCountTime;
        
    }
}
