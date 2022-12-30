using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeatEscapeDoor : MonoBehaviour
{
    Animation animation;

    void Start()
    {
        animation = GetComponent<Animation>();
    }

    public void OpenEscapeDoor()
    {
        animation.Play();
    }
}
