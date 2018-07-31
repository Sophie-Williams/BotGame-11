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

    void OnTriggerEnter(Collider collision)
    {
        if (Schuetze == null)
        {
            Schuetze = FindStatus(collision.transform);
        }

        //Debug.Log("Schuetze status: " + Schuetze);
        //Debug.Log("OnTriggerEnter status: " + collision.transform.parent.GetComponent<status>());
        if (collision.tag == "Player")
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
                    DestroyObject(transform.gameObject);
                }
            }
        }
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