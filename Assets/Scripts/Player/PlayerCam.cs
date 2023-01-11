using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    //look sensitivityy
    [Header("Sensitivity")]
    public float sensX; 
    public float sensY;
    private string mouseStrX;
    private string mouseStrY;

    //player orientation
    [Header("References")]
    public Transform orientation;
    public Transform camHolder;

    float xRotate;
    float yRotate;
    // Start is called before the first frame update
    private void Start()
    {
        mouseStrX = "Mouse X";
        mouseStrY = "Mouse Y";
        //lock cursor in middle.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isDied)
            return;

        float mouseX = Input.GetAxisRaw(mouseStrX) * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw(mouseStrY) * Time.deltaTime * sensY;

        yRotate += mouseX;
        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f); //make sure screen doesnt look pass 90 degrees up or down

        camHolder.rotation = Quaternion.Euler(xRotate, yRotate, 0);
        orientation.rotation = Quaternion.Euler(0, yRotate, 0);
    }

    public void DoFovChanges(float endValue)        //change fov when wall running
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)     //tilt to side when wall running
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }

    public void MoveHead(Vector3 headMoveAmt)       //moves the head away from wall to avoid clipping on wall
    {
        transform.localPosition = headMoveAmt;
    }

    public void ReturnHeadPos()
    {
        transform.localPosition = new Vector3(0,0,0);
    }

    public void ClimbUpMotion()
    {
        transform.DOLocalRotate(new Vector3(45, 0, 0), 0.25f);
    }

    public void ClimbDoneMotion()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 0), 0.25f);
    }

}
