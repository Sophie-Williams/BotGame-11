using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class shoot : NetworkBehaviour {

    public Rigidbody Projectile;
    public float speed = 10f;
    public double shootDelay = .9; //in Millisec
    public float lastShoot = 0;
    public Transform Gun;

    void Start()
    {
        if(Projectile == null)
        {
            Debug.Log("Achtung! Projectile is null.");
        }
    }

    [Command]
    public void CmdFire()
    {
        if (canShoot())
        {
            if(Gun == null)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    if(transform.GetChild(i).name == "Gun" )
                    {
                        Gun = transform.GetChild(i);
                    }
                }
                if (Gun == null)
                {
                    Debug.Log("Gun not found as a Child of the Player");
                }
            }
            Rigidbody projectileClone = Instantiate(Projectile, Gun.position, new Quaternion(Gun.rotation.x , Gun.rotation.y, Gun.rotation.z, Gun.rotation.w));

            projectileClone.GetComponent<projectile>().Schuetze = transform.gameObject;

            NetworkServer.Spawn(projectileClone.gameObject);
            lastShoot = Time.time;
        }
    }

    bool canShoot()
    {
        
        return lastShoot + shootDelay < Time.time;
    }

}
