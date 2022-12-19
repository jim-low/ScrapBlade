using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Boss Movement")]
    private float movementSpeed;
    public float walkSpeed;
    public float runSpeed;

    [Header("Boss Mechanic")]
    public float distanceToPlayer;
    public bool follow;
    private Vector3 moveDir;

    [Header("References")]
    public Transform player;
    Rigidbody rb;

    public BossState state;

    public enum BossState
    {
        running, 
        shooting,
        walking,
        death
    }

    public bool running;
    public bool shooting;
    public bool walking;
    public bool death;

    // Start is called before the first frame update
    void Start()
    {
        follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            movementSpeed = runSpeed;
        }
        else if (walking)
        {
            movementSpeed = walkSpeed;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        
    }

    private IEnumerator FollowPlayer()
    {
        yield return null;
    }

}
