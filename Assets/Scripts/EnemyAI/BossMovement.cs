using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Boss Movement")]
    private float speed;
    public float walkSpeed;
    public float runSpeed;

    [Header("Boss Mechanic")]
    private float distanceToPlayer;
    public float minFollowDistance;
    public bool follow;
    private Vector3 moveDir;

    [Header("References")]
    public Transform player;
    Rigidbody rb;
    RangedEnemy rangedBehavior;

    // Start is called before the first frame update
    void Start()
    {
        follow = false;
        rb = GetComponent<Rigidbody>();
        rangedBehavior = GetComponent<RangedEnemy>();
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (rangedBehavior.GetInSight())
            Navigation.target = player;
    }

    void FixedUpdate()
    {
        //follow = rangedBehavior.GetInSight();

        //FollowPlayer();
    }

    void FollowPlayer()
    {
        if (!rangedBehavior.GetInSight())
			return;

		if (distanceToPlayer >= minFollowDistance)        //if boss not shooting or distance from player is close enough
		{
            Debug.Log("walkig menacingly towards the player");
        }
    }

    public void SetSpeed(string status)
    {
        if (status == "walk")
            speed = walkSpeed;

        if (status == "run")
            speed = runSpeed;

        if (status == "stop")
            speed = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = follow ? Color.yellow : Color.black;
        Gizmos.DrawWireSphere(transform.position, minFollowDistance);
    }
}
