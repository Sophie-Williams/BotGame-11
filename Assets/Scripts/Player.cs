using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private string Name;
    [SerializeField]
    private int Health = 1;
    [SerializeField]
    private int TeamId = 0;

    public Player(string pName, int pHealth, int pTeamId)
    {      
        Name = pName.Normalize();
        if(pHealth > 0)
        {
            Health = pHealth;
        }
        TeamId = pTeamId;
    }

    public int getTeamId()
    {
        return TeamId;
    }

    public string getName()
    {
        return Name;
    }

    public string toString()
    {
        return "{class:Player, Name = " + Name + ", Health = " + Health + ", TeamId = " + TeamId + "}";
    }

    public void takeDamage()
    {
        if (Health > -1)
        {
            Health = Health - 1;
        }
        if(Health == 0)
        {
            dead();
        }
    }

    /**
     *  Called once upon death (Health == 0)
     **/
    void dead()
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
