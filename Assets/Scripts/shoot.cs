using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {

    public Rigidbody Projectile;
    public float speed = 10f;
    public double shootDelay = 900; //in Millisec
    public float lastShoot = 0;

    void Start()
    {
        if(Projectile == null)
        {
            Debug.Log("Achtung! Projectile is null.");
        }
    }

    public void fire()
    {
        if (canShoot())
        { 
            Rigidbody projectileClone = Instantiate(Projectile, transform.position, new Quaternion(transform.rotation.x ,transform.rotation.y, transform.rotation.z, transform.rotation.w));

            projectileClone.GetComponent<projectile>().Schuetze = transform.parent.gameObject;
            lastShoot = Time.time;
        }
    }

    bool canShoot()
    {
        
        return lastShoot + shootDelay < Time.time;
    }

}
