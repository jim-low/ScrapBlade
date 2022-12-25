using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Things handled:
// Restart level
// Go to main menu (we dont have it yet)
// Go to settings page (we dont have it yet)

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

    public void MainMenu()
    {

        Debug.Log("currently this does nothing");
        buttonSource.Play();
    }

    public void SettingPage()
    {
        Debug.Log("currently this does nothing");
        buttonSource.Play();
    }
}
