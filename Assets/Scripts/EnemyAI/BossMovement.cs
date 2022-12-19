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
    private float distanceToPlayer;
    public float followDistance;
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
        distanceToPlayer = Vector3.Distance(player.position, transform.position);       ///get distance from boss to player

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
        FollowPlayer();
    }

    private void StateHandler()
    {
        if (death)      //if boss is defeated
        {
            state = BossState.death;
            follow = false;
        }
        else if (walking)       //early boss stage ( boss will walk while fighting)
        {
            state = BossState.walking;
            movementSpeed = walkSpeed;
        }
        else if(running)        //after few hits ( boss will speed up)
        {
            state = BossState.running;
            movementSpeed = runSpeed;
        }
        else if (shooting)      //if player too far or during certain interval ( boss will shoot)
        {
            state = BossState.shooting;
            follow = false;
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;      //get direction of where the player is
        
        //Quaternion lookDirection = Quaternion.LookRotation(direction);
        
        if (!shooting && distanceToPlayer <= followDistance)        //if boss not shooting or distance from player is close enough
        {
            rb.AddForce(direction * movementSpeed * 20f, ForceMode.Force);      //head towards the player
            follow = true;
        }

        
    }

}
