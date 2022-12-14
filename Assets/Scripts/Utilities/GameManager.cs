using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Things handled:
// Pause Game
// Resume Game
// Restart level
// Go to main menu
// Start game (load StartCutScene)

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
        Cursor.visible = paused || Player.isDied;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;

        if (!Player.isDied && Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        paused = true;
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Pause(); // stop music
        Time.timeScale = 0; // stop gameplay
        Cursor.lockState = CursorLockMode.None; // release mouse lock
        inGameMenu.SetActive(true); // show pause menu
    }

    public void ResumeGame()
    {
        paused = false;
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

        if (GameObject.Find("Enemies") == null) // got no enemies (just boss)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload boss scene
        }
        else // if got enemies
        {
            GameObject enemies = GameObject.Find("Enemies");
            for (int i = 0; i < enemies.transform.childCount; ++i)
            {
                enemies.transform.GetChild(i).GetComponent<Animator>().enabled = true;
                enemies.transform.GetChild(i).GetComponent<RangedEnemy>().SetLiveStatus(true);
            }
            //foreach (Transform enemy in GameObject.Find("Enemies").GetComponentsInChildren<Transform>())
            //{
            //    enemy.GetComponent<Animator>().enabled = true;
            //    enemy.GetComponent<RangedEnemy>().SetLiveStatus(true);
            //}

            GameObject player = GameObject.Find("Player");
            player.GetComponent<Player>().UnDie();
            player.GetComponent<SpawnCheckpoint>().SpawnAtCheckpoint();
        }
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
        LoadSceneAndPlayClick("BossCutScene");
    }
}
