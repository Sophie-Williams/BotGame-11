using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class status : NetworkBehaviour
{
    [SerializeField]
    [SyncVar]
    private int Health = 3;
    [SerializeField]
    [SyncVar]
    private bool alive = true;

    public int getHealth()
    {
        return Health;
    }

    public void takeDamage()
    {
        if (Health > -1)
        {
            Health = Health - 1;
        }
        if (Health == 0)
        {
            dead();
        }
    }
    public void reset(int pHealth)
    {
        Health = pHealth;
        alive = true;
    }

    public bool isAlive()
    {
        return alive;
    }

    /**
     *  Called once upon death (Health == 0)
     **/
    void dead()
    {
        alive = false;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
