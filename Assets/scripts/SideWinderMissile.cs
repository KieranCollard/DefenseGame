using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWinderMissile : MissileBaseObject {

    public float magnitude = 1.0f; //THe size of the wave;
    public float frequency = 2.0f; //The frequency of the sine wave

    public Vector3 sineAxis = Vector3.right;
    public Vector3 moveAxis = -Vector3.up;
	// Use this for initialization
	void Start () {
		if(this.GetComponent<Collider>() == null)
        {
            Debug.Log("The side winder did not have acollider attached");


        }
	}
	
	// Update is called once per frame
	void Update () {
        //use sine to give us a nice wave motion
        Vector3 direction = sineAxis * Mathf.Sin(Time.time * frequency) * magnitude;
        //make sure to also move down the screen
        direction += moveAxis;


        //rotate us 'forwards'
        this.transform.up = direction.normalized;

        Debug.Log(this.transform.forward);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

    }
}
