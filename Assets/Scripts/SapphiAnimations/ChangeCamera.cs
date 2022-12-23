using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    [SerializeField] private GameObject[] _vCams = new GameObject[3];

    float deltaSeconds = 0;
    float SecondsPassed = 0;

    //cameras
    string cam1 = "cam1";
    string cam2 = "cam2";
    string cam3 = "cam3";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaSeconds += Time.deltaTime;
        SecondsPassed = Mathf.FloorToInt(deltaSeconds % 60);
        Debug.Log(SecondsPassed);

        if (SecondsPassed == 5)
        {
            for (int i = 0; i < _vCams.Length; i++)
            {
                //camera 1
                if (_vCams[i].name == cam1)
                {
                    Debug.Log("Cam 1 Triggered");
                    _vCams[i].SetActive(false); //close thy cam
                    BossManager.currentScene = 2;
                }
            }
        }

        if (SecondsPassed == 12)
        {
            for (int i = 0; i < _vCams.Length; i++)
            {
                //camera 1
                if (_vCams[i].name == cam2)
                {
                    Debug.Log("Cam 2 Triggered");
                    _vCams[i].SetActive(false); //close thy cam
                    BossManager.currentScene = 3;
                }
            }
        }



    }
}
