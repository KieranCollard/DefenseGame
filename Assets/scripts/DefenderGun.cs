using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderGun : MonoBehaviour {

    //reference to our bullet prefab
    public GameObject bullet;

    AudioSource audioComponent = null;
    // Use this for initialization
    void Start () {
		if(bullet == null)
        {
            Debug.LogAssertion("The Defender gun has not been assigned a bullet prefab");
        }
        audioComponent = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        //get direction vector from us to mouse
        Vector3 pivotPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        float xDifference = Input.mousePosition.x - pivotPosition.x;
        float yDifference = Input.mousePosition.y - pivotPosition.y;
        //calculate the angle based on direction
        float angle = Mathf.Atan2(yDifference, xDifference) * Mathf.Rad2Deg;
        //establish rotation around z axis
        //offset by 90 to point 'up' instead of left
        //and clamp so that we can't point down
        angle -= 90;
        if (angle > 90 || angle < -180)
        {
            //setting to 89 instead of 90 helps us to preserve the 'up' vector how we would expect
            angle = 89;
        }
        else if (angle < -90 && angle > -180)
        {
            angle = -89;
        }

        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        if (Input.GetMouseButtonDown(0))
        {
            //set the bullet as our child so that it can reference our rotation via parent command
            GameObject test = GameObject.Instantiate<GameObject>(bullet, this.transform.position, this.transform.rotation, this.transform);
            if (audioComponent != null)
            {
                audioComponent.PlayOneShot(audioComponent.clip);
            }
        }
	}

}