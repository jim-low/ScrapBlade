using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip footSteps;
    public AudioClip jumpSound;
    

    public void PlayFootStepsSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = footSteps;
        audio.Play();
    }

    public void PlayJumpSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = jumpSound;
        audio.Play();
    }
}
