using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed =0;
    public float lifeTime =0;

    private float currentCountDown = 0;
    private Vector3 travelDirection;
	// Use this for initialization
	void Start () {
	
        if(this.transform.parent == null)
        {
            Debug.LogAssertion("The bullet did not have an associated parent object");
            Destroy(this);
        }
        else
        {
            this.transform.rotation = this.transform.parent.rotation;
            //ovveride unwanted axis
            travelDirection = new Vector3(0, this.transform.parent.up.y, 0).normalized;
            //unparent after this to prevent bullets following the guns rotation
            this.transform.parent = null;
            currentCountDown = lifeTime;
            StartCoroutine(CountDown());
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(currentCountDown <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
        //move forward at speed until lifetime runs out.
        this.transform.Translate(travelDirection * speed * Time.deltaTime);
	}

    public IEnumerator CountDown()
    {
        while (currentCountDown >=0 )
        {
            --currentCountDown;
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        //TODO play sounds or particles 
    }
}
