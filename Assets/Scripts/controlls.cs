using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlls : MonoBehaviour {
   
    movement myMovement;
    // Use this for initialization
    void Start () {
        myMovement = this.GetComponent<movement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (myMovement == null)
        {
            myMovement = this.GetComponent<movement>();
            if (myMovement == null)
            {
                Debug.Log("myMovement in controlls is null!");
                return;
            }
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            myMovement.right();
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            myMovement.left();
        }

        if(Input.GetAxis("Vertical")>0)
        {
            myMovement.forward();
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            myMovement.backward();
        }
	}
}
