using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(GoToBossScene());
        }
    }

    IEnumerator GoToBossScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BossCutScene");
    }
}
