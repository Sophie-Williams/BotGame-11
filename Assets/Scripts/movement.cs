using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public float rotSpeed = 1.0f;
    public float movementSpeed = 1.0f; 

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
        myRidgidbody.AddRelativeTorque(Vector3.up * -rotSpeed);
    }

    public void right()
    {
        myRidgidbody.AddRelativeTorque(Vector3.up * rotSpeed);
    }

    public void backward()
    {
        myRidgidbody.AddRelativeForce(Vector3.forward * -movementSpeed);
    }

    public void forward()
    {
        myRidgidbody.AddRelativeForce(Vector3.forward * movementSpeed);
    }
}


