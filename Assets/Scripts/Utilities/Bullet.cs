using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddRelativeForce(transform.forward * speed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
        Destroy(gameObject);
    }
}
