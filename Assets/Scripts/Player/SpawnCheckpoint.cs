using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheckpoint : MonoBehaviour
{
    public static Transform checkpoint;
    public bool spawnAtCheckpoint = false;

    public void SpawnAtCheckpoint()
    {
        transform.position = checkpoint.transform.position;
    }
}
