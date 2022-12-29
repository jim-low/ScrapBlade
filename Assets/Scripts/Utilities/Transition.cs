using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public string scene;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(GoToScene(scene));
        }
    }

    IEnumerator GoToScene(string scene)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }
}
