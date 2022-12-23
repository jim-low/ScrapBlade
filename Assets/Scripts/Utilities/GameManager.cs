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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
