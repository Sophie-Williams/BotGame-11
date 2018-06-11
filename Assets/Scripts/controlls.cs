using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlls : NetworkBehaviour
{
   
    public movement myMovement;
    public shoot myShoot;
    // Use this for initialization
    void Start () {
        
        myMovement = this.GetComponent<movement>();
        myShoot = this.GetComponentInChildren<shoot>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        if (myShoot == null)
        {
            myShoot = this.GetComponent<shoot>();
            if (myShoot == null)
            {
                Debug.Log("myShoot in controlls is null!");
                return;
            }
        }
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

        if(Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Peng Peng");
            myShoot.fire();
        }
	}
}
