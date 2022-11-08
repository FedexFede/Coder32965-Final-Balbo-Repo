using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // get player input
    // use it to move sphere
    // set car to sphere position

    //fix rotation and fix gravity of car
    //raycast 

    //Bug: Cuando el auto se voltea no hay funcion de respawn.

    private float  moveInput;
    private float turnInput;
    private bool isCarGrounded;
    
    public float fwdSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;
    
    public float jumpPower;

    public float airDrag;
    public float groundDrag;

    public GameObject cam1;
    public GameObject cam2;

    //reference to sphere rigid body
    public Rigidbody sphereRB;

    void Start()
    {
        //detach rigidbody from car
        sphereRB.transform.parent = null;
        
        cam2.SetActive(true);
        cam1.SetActive(false);


    }

    private void Update()
    {
        Debug.Log("In update");
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        // forma extra corta de If, If moveInput > 0, moveInput *= moveInput * fwdSpeed, sino moveInput *= moveInput * reverseSpeed
        moveInput *= moveInput > 0 ? fwdSpeed : reverseSpeed;
        Debug.Log(moveInput);
        

        //set cars position to sphere
        transform.position = sphereRB.transform.position;

        //set cars rotation
        // Input.GetAxisRaw("Vertical") = Esto da 0 cuando no estamos avanzando, entocnes logramos no rotra si no estamos avanzando.
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);

        // raycast ground check
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit,1f, groundLayer);

        //rotate car to be parallel to ground
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if(isCarGrounded)
        {
            sphereRB.drag = groundDrag;
        }
        else
        {
            sphereRB.drag = airDrag;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCam();
        }

    }

    private void FixedUpdate()
    {
        Debug.Log("In fixedupdate");
        if (isCarGrounded)
        {
            //move car
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
            
        }
        else
        {
            //add extra gravity since flying
            sphereRB.AddForce(transform.up * -9.8f);
        }

        if (isCarGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            sphereRB.AddForce(transform.up * 9.8f);
        }
        
    }

    public void ToggleCam()
    {
        if (cam1.activeInHierarchy)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
        else if (cam2.activeInHierarchy)
        {
            cam2.SetActive(false);
            cam1.SetActive(true);
        }
    }
    
    void ManageJump()
    {


    }
}

