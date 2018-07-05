using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using UnityEngine;

public class projectile : NetworkBehaviour
{
    [SerializeField]
    private int projectileSpeed = 10;
    public GameObject Schuetze;

    Rigidbody myRidgidbody;
    public bool friendlyfire = true;    //On means you can't hit teammates or yourself
    public bool leftPlayer = false;
    double length = 2;

    void Projectile(GameObject absender)
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
            Schuetze = collision.transform.parent.gameObject;
        }
        if (collision.tag == "Player" && collision.transform.parent.gameObject != Schuetze ||
            collision.tag == "Player" && collision.transform.parent.gameObject == Schuetze && leftPlayer)
        {
            if(friendlyfire && collision.transform.parent.GetComponent<Player>().getTeamId() != Schuetze.GetComponent<Player>().getTeamId() || 
                !friendlyfire)
            {
                collision.transform.parent.GetComponent<Player>().takeDamage();
                DestroyObject(transform.gameObject);
            }
        }
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