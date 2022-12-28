using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    [SerializeField] private GameObject[] _vCams = new GameObject[9];

    float deltaSeconds = 0;
    float SecondsPassed = 0;

    //object grabbing
    GameObject player;
    GameObject boss;
    GameObject sword;

    //cameras
    string cam1 = "cam1";
    string cam2 = "cam2";
    string cam3 = "cam3";
    string cam4 = "cam4";
    string cam5 = "cam5";
    string cam6 = "cam6";
    string cam7 = "cam7";
    string cam8 = "cam8";
    string cam9 = "cam9";

    //walking function of player
    bool playerIsWalking = false;
    bool bossIsWalking = false;

    void playerWalk() {
        //translate to front
        player.transform.Translate((Vector3.forward * Time.deltaTime)*2);
    }

    void bossWalk()
    {
        //translate to front
        boss.transform.Translate((Vector3.forward * Time.deltaTime));
    }

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        boss = GameObject.Find("BigBoss");
        sword = player.transform.Find("CameraHolder").Find("PlayerCam").Find("SwordSpawnPoint").Find("PlayerSword").gameObject; //WIP
        boss.active = false;

        playerIsWalking = true;

        //load cinematic bars
        CinematicBars.enableBars = true;
    }

    // Update is called once per frame
    void Update()
    {
        //convert to readable seconds
        deltaSeconds += Time.deltaTime;
        SecondsPassed = Mathf.FloorToInt(deltaSeconds % 60);

        //check if player is walking
        if (playerIsWalking) playerWalk();

        if (bossIsWalking) bossWalk();

        if (SecondsPassed == 2) //left
        {
            //player stops walking
            playerIsWalking = false;

            //playsound


            //disable cam
            _vCams[0].SetActive(false); //close thy cam

            //BossManager.currentScene = 2;
        }

        else if (SecondsPassed == 4) //right
        {
            _vCams[1].SetActive(false); //close thy cam
        }

        else if (SecondsPassed == 6) //mid
        {
            _vCams[2].SetActive(false); //close thy cam
        }

        else if (SecondsPassed == 7)
        {
            //spawn boss
            boss.active = true;

            //play walking sound

            //boss moves forward
            bossIsWalking = true;

            //boss plays walking animation
            BossManager.currentScene = 1;
            
        }

        else if (SecondsPassed == 9)
        {
            _vCams[3].SetActive(false); //close thy cam
        }

        else if (SecondsPassed == 13) //transistion to boss camera.
        {
            //disable the sword
            sword.SetActive(false);
            _vCams[4].SetActive(false); //close thy cam
        }



    }
}
