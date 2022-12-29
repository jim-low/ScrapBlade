using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreTimer : MonoBehaviour
{
    //script must be placed in the text timer to persist throughout scenes 

    static float deltaSeconds = 0;
    static int secondsPassed = 0;
    static int minutesPassed = 0;
    static string finalHighestTime = "99:99";
    static string onCountTime;
    static int highestTime = 1000000;

    //string variables
    string substituteZero = "0";
    string colon = ":";
    string minutes = " minutes ";
    string seconds = " seconds ";
    string empty = "";

    //indication of the end of game
    static bool gameEnd = false;
    static bool isLevel1 = false;

    //object grabbing
    GameObject timeText;
    GameObject displayHighScore;

    //varibles to stop the timer and process the highest score 
    string curScene;
    string creditScene = "CreditScene";
    string levelOne = "Level1";
    string fastestTime = "Fastest Time\n";
    string yourTime = "\nYour Time\n";

    // Start is called before the first frame update
    void Start()
    {
        curScene = SceneManager.GetActiveScene().name;

        //get the text object
        timeText = GameObject.Find("PlayerUI").transform.Find("Timer").gameObject;

        //if it is at the credits Scene, stop the timer.
        if (curScene == creditScene)
        {
            displayHighScore = GameObject.Find("HighScoreBoard").transform.Find("Canvas5").Find("Text5").gameObject;
            gameEnd = true;
            isLevel1 = false;
        } 

        else if (curScene == levelOne)
        {
            gameEnd = false;
            deltaSeconds = 0;
            secondsPassed = 0;
            minutesPassed = 0;
            onCountTime = substituteZero + substituteZero + colon + substituteZero + substituteZero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if 60 seconds have passed.
        if (secondsPassed >= 60)
        {
            secondsPassed -= 60; //remove count
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

        //if the game ends, stop counting the timer, display in the credits scene
        if (gameEnd)
        {
            //check if the time is faster, overwrite if it is
            if ((secondsPassed + (minutesPassed * 60)) < highestTime)
            {
                highestTime = (secondsPassed + (minutesPassed * 60));
                //format and convert to string
                finalHighestTime = minutesPassed.ToString() + minutes + secondsPassed.ToString() + seconds;
            }

            //get the text and display
            displayHighScore.GetComponent<UnityEngine.UI.Text>().text = fastestTime + finalHighestTime + yourTime + onCountTime;

            //remove the text on upper left
            timeText.GetComponent<UnityEngine.UI.Text>().text = empty;
        }

        else
        {
            //count and convert to seconds and minutes passed. 
            deltaSeconds += Time.deltaTime;
            secondsPassed = Mathf.FloorToInt(deltaSeconds % 60); //secondsPassed will update in seconds. 
            minutesPassed = Mathf.FloorToInt(deltaSeconds / 60); //minutesPassed will update in minutes. 

            // display the time
            timeText.GetComponent<UnityEngine.UI.Text>().text = onCountTime;
        }



    }
}
