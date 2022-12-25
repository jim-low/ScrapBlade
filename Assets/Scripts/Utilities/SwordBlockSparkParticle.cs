using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlockSparkParticle : MonoBehaviour
{
    private ParticleSystem sparks;

    void Start()
    {
        sparks = GetComponent<ParticleSystem>();
    }

    public void Spark(Vector3 position)
    {
        sparks.transform.position = position;
        sparks.Play();
    }
}
