using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBaseObject : MonoBehaviour {

    public float pointScore = 0;
    public float speed = 1;

    public Transform bulletReference;
    public GameObject groundReference;


    protected Rigidbody myRigidBody;
    // Use this for initialization
    void Start () {
		if(bulletReference == null)
        {
            Debug.LogAssertion("Missile object did not correctly have the bullet reference " + this.gameObject.name);

        }
        if(groundReference == null)
        {
            Debug.LogAssertion("Missile object did not have the ground reference " + this.gameObject.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShotFromSky()
    {
        Destroy(this.gameObject);
        //TODO add points via event
    }

    public void MadeItToGround()
    {
        Destroy(this.gameObject);
        //TODO subtract lives via event
    }
}
