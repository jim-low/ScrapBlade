using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerWallRun))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float sprintSpeed;
    public float friction;
    public float slideSpeed;
    public float wallRunSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCd;
    public float airMultiplier;
    bool readyToJump;

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
    public bool grounded;
    public bool canSlide;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool slopeJump;

    public Transform orientation;

    [Header("References")]
    [SerializeField]
    private PlayerWallRun wallRunScript;

    //get movement input
    float xInput;
    float yInput;

    Vector3 moveDir;
    Rigidbody rb;

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

    public bool climbing;
    public bool crouching;
    public bool sliding;
    public bool wallRunning;
    public bool inAir;

    // Start is called before the first frame update
    void Start()
    {
        wallRunScript = GetComponent<PlayerWallRun>();
        rb = GetComponent<Rigidbody>();
        readyToJump = true;
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        MyInput();
        SpeedControl();
        StateHandler();

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
        
        MovePlayer();
        
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
        if (grounded)       //mode sprinting
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
            canSlide = true;
            inAir = false;
        }
        else if(crouching)       //mode crouching
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
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
        if (OnSlope() && !slopeJump && !climbing && !wallRunning)
        {
            rb.useGravity = false;
            rb.AddForce(GetSlopeMoveDirection(moveDir) * moveSpeed * 20f, ForceMode.Force);

            if(rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
       
        //if normal ground
        if (grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded && !wallRunScript.wallDetected)
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
