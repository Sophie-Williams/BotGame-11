using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class firstBot : NetworkBehaviour
{

    public movement myMovement;
    public shoot myShoot;
    public Transform Players;
    public Transform Projectiles;

    // Use this for initialization
    void Start()
    {

        myMovement = this.GetComponent<movement>();
        myShoot = this.GetComponentInChildren<shoot>();
        Players = GameObject.Find("Players").transform;
        Projectiles = GameObject.Find("Projectiles").transform;
    }

    // Update is called once per frame
    void Update()
    {
        List<Transform> targets = getOtherPlayers();
        if(targets.Count > 0)
        {
            turnTowards(targets[0].position);
            myShoot.CmdFire();
        }
    }

    public void turnTowards(Vector3 v3)
    {

        Vector3 deltaPosition = v3 - transform.position; //new Vector2(v3.x - transform.position.x, v3.z - transform.position.z);
        float targetRotation = Vector2.SignedAngle(new Vector2(transform.forward.x, transform.forward.z), new Vector2 (deltaPosition.x, deltaPosition.z));
        //Debug.Log("trying to turn towards: " + v3.ToString() + " , deltaPosition: " + deltaPosition + " , targetRotation: " + targetRotation);

        if (targetRotation < 0)
        {
            myMovement.right();
        }
        else if(targetRotation > 0)
        {
            myMovement.left();
        }
        
    }

    public List<Transform> getOtherPlayers()
    {
        List<Transform> OtherPlayers = new List<Transform>();
        for(int i = 0; i < Players.childCount; i++)
        {
            if(Players.GetChild(i) != transform && Players.GetChild(i).gameObject.activeInHierarchy)
            {
                OtherPlayers.Add(Players.GetChild(i));
            }
        }
        return OtherPlayers;
    }

    public List<Transform> getOtherProjectiles()
    {
        List<Transform> OtherProjectiles = new List<Transform>();
        for (int i = 0; i < Projectiles.childCount; i++)
        {
            if (Projectiles.GetChild(i).GetComponent<projectile>().Schuetze != GetComponent<status>() && Projectiles.GetChild(i).GetComponent<projectile>().leftPlayer)
            {
                OtherProjectiles.Add(Projectiles.GetChild(i));
            }
        }
        return OtherProjectiles;
    }
}
