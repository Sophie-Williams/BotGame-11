using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class movement : NetworkBehaviour
{

    static public float rotSpeed = 5.5f;
    static public float maxMovementSpeed = 10f;
    static public float acceleration = 20f;

    Rigidbody myRidgidbody;
    // Use this for initialization
    void Start()
    {
        myRidgidbody = this.GetComponent<Rigidbody>();
    }

  

    // Update is called once per frame
    void Update()
    {
        if (myRidgidbody == null)
        {
            myRidgidbody = this.GetComponent<Rigidbody>();
            if (myRidgidbody == null)
            {
                Debug.Log("myRigidbody in movement is null!");
            }
        }

    }

    public void left()
    {
        transform.Rotate(new Vector3(0, -1 * rotSpeed, 0));
        myRidgidbody.AddForce(Vector3.Cross(-transform.up * rotSpeed, myRidgidbody.velocity));
    }

    public void right()
    {
        transform.Rotate(new Vector3(0, 1 * rotSpeed, 0));
        myRidgidbody.AddForce(Vector3.Cross(transform.up * rotSpeed, myRidgidbody.velocity) );
    }

    public void backward()
    {
        if(myRidgidbody.velocity.magnitude < maxMovementSpeed)
            myRidgidbody.AddRelativeForce(Vector3.forward * -acceleration);
    }

    public void forward()
    {
        if (myRidgidbody.velocity.magnitude < maxMovementSpeed)
            myRidgidbody.AddRelativeForce(Vector3.forward * acceleration);
    }
}


