using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float sprintSpeed;
    public float friction;
    public float slideSpeed;
    public float wallRunSpeed;
    public float originalFov;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCd;
    public float airMultiplier;
    bool readyToJump;
    private Vector3 previousPosition;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.C;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool canSlide;
    public string groundLayer;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool slopeJump;

    [Header("References")]
    public PlayerWallRun wallRunScript;
    public Transform orientation;
    public PlayerCam cam;
    public GameObject player;
    public PlayerSounds playerSound;

    //get movement input
    float xInput;
    float yInput;

    Vector3 moveDir;
    Rigidbody rb;

    [Header("Movement States")]
    public MovementState state;

    public enum MovementState
    {
        sprinting,
        air,
        wallrunning,
        crouching,
        climbing,
        sliding
    }

    public bool grounded;
    public bool climbing;
    public bool crouching;
    public bool sliding;
    public bool wallRunning;
    public bool inAir;

    // Start is called before the first frame update
    void Start()
    {
        cam.DoFovChanges(originalFov);
        rb = GetComponent<Rigidbody>();
        readyToJump = true;
        startYScale = transform.localScale.y;
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isDied)
        {
            return;
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        
        MyInput();
        SpeedControl();
        StateHandler();

        if ((yInput > 0 || yInput < 0 || xInput > 0 || xInput < 0) && (grounded || wallRunning))          //if player is running            
        {
            playerSound.PlayFootStepsSound();
        }

        if(transform.position.y > previousPosition.y + 0.5f)        //if player is falling after jumping
        {
            if (grounded)        //if player landed on ground
            {
                playerSound.PlayJumpSound();
            }
        }

        //apply friction
        if (grounded)
        {
            rb.drag = friction;
        }
        else
        {
            rb.drag = 0;
        }
       
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity, ForceMode.Acceleration);//apply gravity
        
        if ((wallRunScript.wallDetected || wallRunScript.CheckForObstacleWall()) && inAir && !climbing)
        {
            rb.AddForce(Vector3.down * 35f, ForceMode.Force);
        }
        MovePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer(groundLayer)) && inAir && !grounded)       //checks if the player is stuck on ground Layered wall
        {
            rb.velocity = Vector3.zero;                                 //disable any force to make the player stick to walls
            rb.AddForce(Vector3.down * 200f, ForceMode.Force);
        }
    }

    private void MyInput()
    {
        //detect moving
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        //when can jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCd);
        }

        //crouching
        if (Input.GetKeyDown(crouchKey))
        {
            crouching = true;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if(Input.GetKeyUp(crouchKey))
        {
            crouching = false;
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            
        }
        
    }

    private void StateHandler()
    {
        if (crouching)       //mode crouching
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
            canSlide = true;
            inAir = false;
        }
        if (grounded)       //mode sprinting
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
            canSlide = true;
            inAir = false;
        }
        else if (wallRunning)     //mode wallrun
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallRunSpeed;
            canSlide = false;
            inAir = false;
        }
        else if (climbing)
        {
            state = MovementState.climbing;
            canSlide = false;
            inAir = false;
        }
        else if (sliding)        //mode sliding
        {
            state = MovementState.sliding;
            canSlide = true;
            inAir = false;

            if (OnSlope() && rb.velocity.y < 0.1f)//if player is on slope and moving downwards
            {
                desiredMoveSpeed = slideSpeed;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }
        else                //in air, falling or jumping
        {
            state = MovementState.air;
            inAir = true;
            canSlide = false;
        }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0) //check if desiredmovespeed changed a lot, ensure the player doesnt immediately change speed
        {
            StartCoroutine(SmoothenMoveSpeed());
        }
        else                //if player not moving fast and stops
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothenMoveSpeed()     //changes the movespeed to the desiredMoveSpeed at a certain pace
    {
        float time = 0;
        float diff = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while(time < diff)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / diff);
            time += Time.deltaTime;
            yield return null;
        }
        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        //calc movement direction
        moveDir = orientation.forward * yInput + orientation.right * xInput;

        //if on slope
        if (OnSlope() && !slopeJump && !climbing && !wallRunScript.wallDetected)
        {
            rb.useGravity = false;
            rb.AddForce(GetSlopeMoveDirection(moveDir) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        //if normal ground
        if (grounded)
        {
            cam.ClimbDoneMotion();
            if (wallRunScript.wallDetected && yInput > 0)
            {
                if (wallRunScript.wallLeft)
                {
                    rb.AddForce(Vector3.left * moveSpeed * 10f, ForceMode.Force);
                }
                else if (wallRunScript.wallRight)
                {
                    rb.AddForce(Vector3.right * moveSpeed * 10f, ForceMode.Force);
                }
            }
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        
        else if (!grounded || (!grounded && !wallRunScript.wallDetected))
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        //turn on gravity when not wall runnnig or not on slope
        if (!wallRunning && !OnSlope())
        {
            rb.useGravity = true;
        }
        //speedometer
        PlayerSpeedometer.speedMsg = "Speed:" + rb.velocity.magnitude;
    }

    private void SpeedControl()
    {
        //limit velocity when on slope
        if (OnSlope() && !slopeJump)
        {
            if (rb.velocity.magnitude > desiredMoveSpeed)
            {
                rb.velocity = rb.velocity.normalized * desiredMoveSpeed;
            }
        }
        else        //if not on slope
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //limit velocity when hit max speed
            if (flatVel.magnitude > desiredMoveSpeed)
            {
                Vector3 limitedSpeed = flatVel.normalized * desiredMoveSpeed;
                rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, limitedSpeed.z);
            }
        }

    }

    private void Jump()
    {
        slopeJump = true;
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        previousPosition = transform.position;

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        slopeJump = false;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }                               

        return false;

    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

}
