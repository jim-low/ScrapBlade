using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public PlayerMovement playerMovement;
    public PlayerWallRun playerWallRun;
    public PlayerCam cam;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;
    public float climbTiltAmt;
    public float climbFOVAmt;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;
    public bool topWall;
    
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

        topWall = Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), orientation.forward, out frontWallHit, detectionLength, whatIsWall);
    }

    private void StateMachine()
    {
        //climbing

        if(wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            if(!playerMovement.climbing && climbTimer > 0)
            {
                StartClimb();
                
            }else if(playerMovement.climbing)
            {
                if (!topWall)       //if the player climbs up to the ground
                {
                    StartCoroutine(ClimbOverMovement());
                }
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
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);   //check if wall in front is big enough
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);           //get the wall normal

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
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);              //sets the velocity upwards to climb
        cam.DoFovChanges(climbFOVAmt);
        
    }

    private IEnumerator ClimbOverMovement()
    {
        transform.Translate(0, 1.0f, 0);                                         //pushes the player up the cliff
        //rb.AddForce(Vector3.up * 40.0f, ForceMode.Force);
        cam.ClimbUpMotion();                                                   //rotate the camera down 45 degree
        yield return new WaitForSeconds(0.5f);
        //rb.AddForce(Vector3.forward * 10.0f, ForceMode.Force);                         //pushes the player front and over the cliff
        cam.ClimbDoneMotion();
    }

    private void StopClimb()
    {
        cam.DoFovChanges(playerMovement.originalFov);
        playerMovement.climbing = false;
    }



}
