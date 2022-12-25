using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Things handled:
// Restart level
// Go to main menu (we dont have it yet)
// Go to settings page (we dont have it yet)

public class GameManager : MonoBehaviour
{
    public void Restart()
    {
        Player.isDied = false;
        GameObject.Find("PlayerUI").transform.Find("Buttons").gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {

        Debug.Log("currently this does nothing");
    }

    public void SettingPage()
    {
        Debug.Log("currently this does nothing");
    }
}
