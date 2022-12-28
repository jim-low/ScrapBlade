using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartingCutscene : MonoBehaviour
{
    [SerializeField] private GameObject[] _vCams = new GameObject[6];

    //timer
    float deltaSeconds = 0;
    float SecondsPassed = 0;

    //scene duration
    int cam1Duration = 3;
    int cam2Duration = 4;
    int cam3Duration = 5;
    int cam4Duration = 7;
    int cam5Duration = 9;
    int cam6Duration = 11;

    //cameras
    string cam1 = "CamPoint1";
    string cam2 = "CamPoint2";
    string cam3 = "CamPoint3";
    string cam4 = "CamPoint4";
    string cam5 = "CamPoint5";
    string cam6 = "CamPoint6";

    GameObject playerGameObject;
    GameObject UiGameObject;
    string levelChange = "Level1";

// Start is called before the first frame update
void Start()
    {

        ////enable cinematic cameras.
        //for (int x = 0; x < 6; x++)
        //{
        //    _vCams[x].SetActive(true);
        //}

        CinematicBars.enableBarsImmediate = true; //show da cinematic bars
        //disable scripts and objects
        playerGameObject = GameObject.Find("Player");
        playerGameObject.GetComponent<Interact>().enabled = false;
        playerGameObject.GetComponent<PlayerMovement>().enabled = false;
        playerGameObject.GetComponent<PlayerSlide>().enabled = false;
        playerGameObject.GetComponent<PlayerWallRun>().enabled = false;
        playerGameObject.GetComponent<PlayerClimbing>().enabled = false;
        UiGameObject = GameObject.Find("PlayerUI");
        UiGameObject.active = false;
    }

    // Update is called once per frame
    void Update()
    {

        //calculate seconds passed
        deltaSeconds += Time.deltaTime;
        SecondsPassed = Mathf.FloorToInt(deltaSeconds % 60);

        //switch scene with each second or few seconds
        if (SecondsPassed == 3) //close first scene 
        {
            _vCams[0].SetActive(false);
        }

        else if (SecondsPassed == 4) //close second scene at x seconds
        {
            _vCams[1].SetActive(false);
        }

        else if (SecondsPassed == 5) //close scene at x seconds
        {
            _vCams[2].SetActive(false);
        }

        else if (SecondsPassed == 7) //close scene at x seconds
        {
            _vCams[3].SetActive(false);
        }

        else if (SecondsPassed == 9) //close scene at x seconds
        {
            _vCams[4].SetActive(false);
        }

        else if (SecondsPassed == 11) //close second scene at x seconds
        {
            _vCams[5].SetActive(false);
            CinematicBars.disableBars = true;
            SceneManager.LoadScene(levelChange);

            //reenable scripts and objects
            //playerGameObject.GetComponent<Interact>().enabled = true;
            //playerGameObject.GetComponent<PlayerMovement>().enabled = true;
            //playerGameObject.GetComponent<PlayerSlide>().enabled = true;
            //playerGameObject.GetComponent<PlayerWallRun>().enabled = true;
            //playerGameObject.GetComponent<PlayerClimbing>().enabled = true;
            //UiGameObject.active = true;
        }
    }
}
