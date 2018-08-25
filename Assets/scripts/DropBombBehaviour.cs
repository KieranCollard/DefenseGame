using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombBehaviour : MissileBaseObject {

    //pad the sides of screen slightly to stop appearing outside or partially cut off
    public float screenPaddingSides = 0;
    //amount 'above' screen to spawn
    public float screenPaddingTop = 0;

    // Use this for initialization
    void Start () {
        //confirm we have a collider
        if (this.GetComponent<Collider>() == null)
        {
            Debug.LogAssertion("The DropBomb prefab did not have a collider");
        }
        //spawn within bounds of screen  plusor minus padding
        float xMin = screenPaddingSides;
        float xMax = Screen.width - screenPaddingSides;
        float yValue = Screen.height + screenPaddingTop;

        float xValue = Random.Range(xMin, xMax);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(xValue, yValue));
        //reset z position
        worldPosition.z = 0;

        this.transform.position = worldPosition;

    }
	
	// Update is called once per frame
	void Update () {
        //just travel forward blindly
        this.transform.Translate(-this.transform.up * speed * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == bulletReference)
        {
            ShotFromSky();
        }
        else if(gameObject == groundReference)
        {
            MadeItToGround();
        }
    }
}
