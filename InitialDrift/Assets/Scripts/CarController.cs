using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public const int FORCE_MULT = 500;

    public Rigidbody rb;
    public GameObject[] wheels;

    public float forwardAccel = 8f;
    public float maxSpeed = 50f;
    public float turnStrength = 180f;

    private float reverseAccel;

    private float fwdInput;
    private float turnInput;

    // Start is called before the first frame update
    void Start()
    {
        rb.transform.parent = null;

        reverseAccel = forwardAccel / 2f;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(fwdInput) > 0) 
        {
            rb.AddForce(transform.forward * fwdInput);
        }
        else 
        {
            //if (Mathf.Abs(rb.velocity.magnitude) <= 0.1f) { rb.velocity *= 0; } 
            //else rb.velocity *= 0.95f;
        }

    }

    private void Update()
    {
        GetInput();

        transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - 0.25f, rb.transform.position.z);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * Time.deltaTime, 0f) * rb.velocity.magnitude / 10);

        if (rb.velocity.magnitude > 0) 
        {
            SpinWheels();
        }
    }

    private void GetInput()
    {
        fwdInput = turnInput = 0f;

        if (Input.GetAxis("Vertical") > 0)
        {
            fwdInput = Input.GetAxis("Vertical") * forwardAccel * FORCE_MULT;
        }
        else if (Input.GetAxis("Vertical") < 0) 
        {
            fwdInput = Input.GetAxis("Vertical") * reverseAccel * FORCE_MULT;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            turnInput = Input.GetAxis("Horizontal") * turnStrength;
        }
        else if (Input.GetAxis("Horizontal") < 0) 
        {
            turnInput = Input.GetAxis("Horizontal") * turnStrength;
        }
    }

    private void SpinWheels()
    {
        foreach (GameObject wheel in wheels) 
        {
            wheel.transform.Rotate(1 * rb.velocity.magnitude / 50,0,0);
        }
    }
}
