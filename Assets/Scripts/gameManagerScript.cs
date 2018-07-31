using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class gameManagerScript : NetworkBehaviour
{

    ArrayList Players;



    // Use this for initialization
    void Start()
    {
        Players = new ArrayList();
    }

    public void ready(NetworkIdentity pNetworkIdentity)
    {
        Players.Add(pNetworkIdentity);
        CmdStartRound();
    }
   
    public void AddPlayer(Player pPlayer)
    {
        Players.Add(pPlayer);
       // startRound();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    void CmdStartRound()
    {
        foreach (NetworkIdentity curPlayer in Players)
        {
            Debug.Log(curPlayer.GetComponent<Player>().getName() + " started Round");
            //curPlayer.GetComponentInChildren<status>().reset(3);

            curPlayer.transform.Find("IngameCharacter").gameObject.SetActive(true);
        }
    }
}
