using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using UnityEngine;

public class projectile : NetworkBehaviour
{
    [SerializeField]
    private int projectileSpeed = 10;
    public status Schuetze;

    Rigidbody myRidgidbody;
    public bool friendlyfire = true;    //On means you can't hit teammates or yourself
    public bool leftPlayer = false;
    double length = 2;
    bool hit = false;       //This prevents the projectile to hit multiple targets at once

    void Projectile(status absender)
    {
        Schuetze = absender;
    }

    // Use this for initialization
    void Start ()
    {
        myRidgidbody = this.GetComponent<Rigidbody>();
        
    }

    public int getProjectileSpeed()
    {
        return projectileSpeed;
    }

    [Server]
    void OnTriggerExit(Collider collision)
    {
        //Debug.Log("left");
        //Debug.Log(Schuetze);
        //Debug.Log(collision.transform.parent.gameObject);
        if(collision.transform.parent.gameObject == Schuetze)
        {
            leftPlayer = true;
        }
    }

    [Server]
    void OnTriggerEnter(Collider collision)
    {
        if (Schuetze == null)
        {
            Schuetze = FindStatus(collision.transform);
        }

        //Debug.Log("Schuetze status: " + Schuetze);
        //Debug.Log("OnTriggerEnter status: " + collision.transform.parent.GetComponent<status>());
        if (collision.tag == "Player" && !hit)
        {
            status ColliderStatus = FindStatus(collision.transform);
            if(ColliderStatus == null)
            {
                Debug.Log("Player Without Collider Error!!!!!");
                
            }


            if (ColliderStatus != Schuetze ||
                ColliderStatus == Schuetze && leftPlayer)
            {
                if (friendlyfire && ColliderStatus != Schuetze ||
                    !friendlyfire)
                {
                    ColliderStatus.takeDamage();
                    Schuetze.score += 1;
                    //Debug.Log("Player took damage: " + ColliderStatus.GetInstanceID() + " he was hit at: " + collision.name + " , Time: "+ Time.time);
                    DestroyObject(transform.gameObject);
                    hit = true;
                }
            }
        }
       /* else if(collision.tag == "Player" && hit)
        {
            Debug.Log("DoubleHit prevented: " + FindStatus(collision.transform).GetInstanceID() + " he was hit at: " + collision.name + " , Time: " + Time.time);
        }*/
    }

    public status FindStatus(Transform pTransform)
    {
        status tempStatus = pTransform.GetComponent<status>();
        if (tempStatus == null)
            tempStatus = pTransform.GetComponentInParent<status>();
        if (tempStatus == null)
            tempStatus = pTransform.GetComponentInChildren<status>();
        return tempStatus;
    }

    [Server]
    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Wall")
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale += new Vector3(0.0f, -Time.deltaTime * myRidgidbody.velocity.magnitude, 0.0f);
                if(leftPlayer == false)
                {
                    transform.localScale += new Vector3(0.0f, -Time.deltaTime * myRidgidbody.velocity.magnitude, 0.0f);
                }
            }
            if (transform.localScale.x < 0 || transform.localScale.y < 0 || transform.localScale.z < 0)
            {
                DestroyObject(transform.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (myRidgidbody == null)
        {
            myRidgidbody = this.GetComponent<Rigidbody>();
            if (myRidgidbody == null)
            {
                Debug.Log("myRigidbody in projectile is null!");
            }
        }
        myRidgidbody.velocity = transform.up * projectileSpeed;


        if (transform.localScale.y < length && leftPlayer == false)
        {
            transform.localScale += new Vector3(0.0f, Time.deltaTime * myRidgidbody.velocity.magnitude, 0.0f);
        }
        

    }
}