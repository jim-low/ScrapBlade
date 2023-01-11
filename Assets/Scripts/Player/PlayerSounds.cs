using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip footSteps;
    public AudioClip jumpSound;
    public AudioSource audio;
    AudioClip noSound;
    public bool toggleSound;


    void Update()
    {
        if(toggleSound && !audio.isPlaying)             //ensure the sound doesnt play when not needed
        {
            toggleSound = false;
            audio.Stop();
            audio.clip = noSound;
        }
    }

    public void PlayFootStepsSound()
    {
        if (!audio.isPlaying)                        //check if there is any existing sound playing ady
        {
            audio = GetComponent<AudioSource>();
            audio.clip = footSteps;
            audio.Play();
        }

    }

    public void StopAudio()
    {
        audio.Stop();
    }

    public void PlayJumpSound()
    {
        toggleSound = true;
        audio = GetComponent<AudioSource>();
        audio.clip = jumpSound;
        audio.Play();
    }
}
