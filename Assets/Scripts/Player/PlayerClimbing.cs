using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public PlayerMovement playerMovement;
    public LayerMask whatIsWall;
    public float climbTiltAmt;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;
    public PlayerCam cam;

    private RaycastHit frontWallHit;
    private bool wallFront;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        StateMachine();

        if (playerMovement.climbing)
        {
            ClimbingMovement();
        }
    }

    private void StateMachine()
    {
        //climbing

        if(wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            if(!playerMovement.climbing && climbTimer > 0)
            {
                StartClimb();
            }
            //timer
            if(climbTimer > 0)
            {
                climbTimer -= Time.deltaTime;
            }
            if(climbTimer < 0)
            {
                StopClimb();
            }
        }
        else
        {
            if (playerMovement.climbing)
            {
                StopClimb();
            }
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        if (playerMovement.grounded)
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimb()
    {
        playerMovement.climbing = true;
    }
    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        StartCoroutine(ClimbTiltMovement());
        
    }

    private IEnumerator ClimbTiltMovement()
    {
        cam.DoTilt(-climbTiltAmt);
        yield return new WaitForSeconds(0.3f);
        cam.DoTilt(climbTiltAmt);
        if (!playerMovement.climbing)
        {
            cam.DoTilt(0f);
        }
    }

    private void StopClimb()
    {
        playerMovement.climbing = false;
    }



}
