using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [Range(0, 100f)]
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
    }
}
