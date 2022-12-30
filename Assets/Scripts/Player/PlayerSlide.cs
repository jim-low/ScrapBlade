using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement playerMovement;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer; 
    private RaycastHit slopeHit;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;
    private string horizontal;
    private string vertical;

    // Start is called before the first frame update
    void Start()
    {
        horizontal = "Horizontal";
        vertical = "Vertical";
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        startYScale = playerObj.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw(horizontal);
        verticalInput = Input.GetAxisRaw(vertical);


        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && playerMovement.canSlide)
        {
            StartSlide();
        }
        if(Input.GetKeyUp(slideKey) && playerMovement.sliding)
        {
            StopSlide();
        }
    }

    private void FixedUpdate()
    {
        if (playerMovement.sliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {

        playerMovement.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);    //scales the player down
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);                                                  //pushes player down to touch the ground

        slideTimer = maxSlideTime;

       
    }

    private void StopSlide()
    {
        playerMovement.sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (!playerMovement.OnSlope() || rb.velocity.y > -0.1f) //apply extra slide speed if player is not on slope or moving upwards
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }
        else //sliding down a slope
        {
            rb.AddForce(playerMovement.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }
        


        if(slideTimer <= 0)
        {
            StopSlide();
        }
    }
}
