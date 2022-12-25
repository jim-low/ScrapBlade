using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Things handled:
// Restart level
// Go to main menu
// Go to settings page
// Play level 1

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    private AudioSource buttonSource;
    public AudioClip buttonClick;

    void Start()
    {
        buttonSource = GetComponent<AudioSource>();
        buttonSource.clip = buttonClick;
    }

    public void Restart()
    {
        Player.isDied = false;
        GameObject.Find("PlayerUI").transform.Find("Buttons").gameObject.SetActive(false);
        buttonSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadSceneAndPlayClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        buttonSource.Play();
    }

    public void MainMenu()
    {
        LoadSceneAndPlayClick("MainMenu");
    }

    public void StartGame()
    {
        LoadSceneAndPlayClick("Level1");
    }
}
