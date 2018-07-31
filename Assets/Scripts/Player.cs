using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    [SyncVar]
    private string Name;

    [SerializeField]
    [SyncVar]
    private int TeamId = 0;

    public void setPlayerOptions(string pName, int pTeamId)
    {      
        Name = pName.Normalize();
        TeamId = pTeamId;
        CmdReady();
    }

    [Command]
    private void CmdReady()
    {
        FindObjectOfType<gameManagerScript>().ready(GetComponent<NetworkIdentity>());
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
        return "{class:Player, Name = " + Name + ", TeamId = " + TeamId + "}";
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
