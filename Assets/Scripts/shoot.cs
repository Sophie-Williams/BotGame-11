using UnityEngine.Networking;
using UnityEngine;

public class shoot : NetworkBehaviour {

    public GameObject Projectile;
    static public double shootDelay = .9; //in Millisec
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

            GameObject projectileClone = (GameObject) Instantiate(Projectile, Gun.position, Gun.rotation);

            projectileClone.GetComponent<projectile>().Schuetze = GetComponent<status>();
            //projectileClone.transform.SetParent(GameObject.Find("Projectiles").transform);
            NetworkServer.Spawn(projectileClone.gameObject);
            lastShoot = Time.time;
        }
    }

    bool canShoot()
    {
        
        return lastShoot + shootDelay < Time.time;
    }

}
