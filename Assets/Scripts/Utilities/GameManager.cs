using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Things handled:
// Restart level
// Go to main menu
// Go to settings page
// Play level 1
// Pause Level
// Resume Level

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    private AudioSource buttonSource;
    public AudioClip buttonClick;
    public GameObject inGameMenu;
    private bool paused = false;

    void Start()
    {
        buttonSource = GetComponent<AudioSource>();
        buttonSource.clip = buttonClick;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Pause(); // stop music
        Time.timeScale = 0; // stop gameplay
		Cursor.lockState = CursorLockMode.None; // release mouse lock
        inGameMenu.SetActive(true); // show pause menu
    }

    public void ResumeGame()
    {
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().UnPause(); // continue music
        Time.timeScale = 1; // continue gameplay
		Cursor.lockState = CursorLockMode.Locked; // lock mouse
        inGameMenu.SetActive(false); // hide pause menu
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
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        LoadSceneAndPlayClick("MainMenu");
    }

    public void StartGame()
    {
        LoadSceneAndPlayClick("Level1");
    }
}
