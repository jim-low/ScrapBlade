using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    [Header("Movement")]
    public bool wallDetected;

    [Header("WallRunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float fovChangeAmt;
    public float camTiltAmt;
    public float camSideMove;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    private float xInput;
    private float yInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private RaycastHit frontWallHit;
    private RaycastHit backWallHit;
    public bool wallLeft;
    public bool wallRight;
    public bool wallFront;
    public bool wallBack;

    [Header("Exit WallRun")]
    private bool exitWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    public PlayerCam cam;
    private PlayerMovement playerMovement;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        exitWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    void FixedUpdate()
    {
        if (playerMovement.wallRunning)
        {
            WallRunningMovement();
        }
    }

    public void CheckForWall()  //check if there is wall on right and left
    {
        wallRight = Physics.Raycast(transform.position - new Vector3(0, 1, 0), orientation.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position - new Vector3(0, 1, 0), -orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
        wallFront = Physics.Raycast(transform.position - new Vector3(0, 1,0), orientation.forward, out frontWallHit, wallCheckDistance, whatIsWall);
        wallBack = Physics.Raycast(transform.position - new Vector3(0, 1, 0), -orientation.forward, out backWallHit, wallCheckDistance, whatIsWall);
        if (wallRight || wallLeft || wallFront || wallBack)
        {
            wallDetected = true;
        }
        else
        {
            wallDetected = false;
        }
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        //detect moving
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        //State1 - wallrunning
        if ((wallLeft || wallRight) && yInput > 0 && AboveGround() && !exitWall)//check for walls, if player is moving forward and aboveground
        {
            //start wallrun
            if (!playerMovement.wallRunning)
            {
                StartWallRun();
            }

            //wall Jump only when wall running
            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
        else if (exitWall) // exit wall run mechanic to wall jump
        {
            if (playerMovement.wallRunning)
            {
                StopWallRun();
            }

            if (exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }
            if (exitWallTimer <= 0)
            {
                exitWall = false;
            }
        }

        else
        {
            if (playerMovement.wallRunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        playerMovement.wallRunning = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //apply camera effects;
        cam.DoFovChanges(fovChangeAmt);
        if (wallLeft)
        {
            cam.DoTilt(-camTiltAmt);
        }
        if (wallRight)
        {
            cam.DoTilt(camTiltAmt);
        }

    }

    private void WallRunningMovement()
    {
        rb.useGravity = useGravity;//to toggle gravity in inspector
        if (wallLeft)
        {
            cam.MoveHead(new Vector3(camSideMove, 0, 0));
        }
        else if (wallRight)
        {
            cam.MoveHead(new Vector3(-camSideMove, 0, 0));
        }

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;    //check which wall to run on
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);             

        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)       //check which way the player is facing in constrast to the wall
        {
            wallForward = -wallForward;
        }


        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);               //forward force

        //push player to wall
        if(!(wallLeft & xInput > 0) && !(wallRight && xInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        //weaken gravity when wall running
        if (useGravity)
        {
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }
       
    }

    private void StopWallRun()
    {
        playerMovement.wallRunning = false;
        //reset camera effects;
        cam.ReturnHeadPos();
        cam.DoFovChanges(playerMovement.originalFov);
        cam.DoTilt(0f);
    }

    private void WallJump()
    {
        //exit wall
        exitWall = true;
        exitWallTimer = exitWallTime;

		playerMovement.wallRunJump = true;

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);        //reset y velocity of player to make jumping feel smooth
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
