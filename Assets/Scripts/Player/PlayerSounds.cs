using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip footSteps;
    public AudioClip jumpSound;
    public AudioSource audio;
    public float footStepDelayTime;

    [Header("References")]
    public PlayerMovement playerMovement;
    
    public void PlayFootStepsSound()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(PlayFootSteps());
    }

    private IEnumerator PlayFootSteps()
    {
        if (playerMovement.isRunning == true)       //check if player is moving on ground
        {
            audio.enabled = true;
            audio.clip = footSteps;
            audio.Play();
            yield return new WaitForSeconds(footStepDelayTime);     //delay footstep

        }
        else
        {
            audio.Stop();
            audio.enabled = false;
        }
    }

    public void StartAudio()
    {
        audio.enabled = true;
    }

    public void PlayJumpSound()  
    {
        audio = GetComponent<AudioSource>();
        audio.clip = jumpSound;
        audio.Play();
    }
}
