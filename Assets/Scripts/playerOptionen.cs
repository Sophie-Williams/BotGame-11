using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class playerOptionen : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void enter()
    {
        string Name = transform.Find("NameInputField").Find("NameText").GetComponent<Text>().text;
        int TeamID = int.Parse(transform.Find("TeamIdInputField").Find("TeamText").GetComponent<Text>().text);

        if(Name.Length > 0 && TeamID > 0)
        {
            Debug.Log("Name: " + Name + ", TeamID: " + TeamID);
            SetPlayerOptions(Name, TeamID);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Please Enter Name and a TeamId > 0");
        }
    }

    private void SetPlayerOptions(string Name, int TeamID)
    {
        if(transform.parent.GetComponentInParent<Player>() == null)
        {
            Debug.Log("Player not found");

        }
        else
        {
            transform.parent.GetComponentInParent<Player>().setPlayerOptions(Name, TeamID);
        }
       
    }

}
