using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBombBehaviour : DropBombBehaviour {

    //time in seconds before the boost occurs
    public float timeBeforeBoost = 2.0f;

    //amount to increase speed by
    public float boostMultiplier = 1.5f;

    ParticleSystem boostParticles = null;
	// Use this for initialization
	void Start () {
		
        if(this.GetComponent<Collider>() == null)
        {
            Debug.LogAssertion("The BostBombBehaviour did not have a collider");
        }

        boostParticles = this.GetComponent<ParticleSystem>();
        if (boostParticles == null)
        {
            Debug.LogAssertion("The boost missile did not have a particle system attached");
        }
        SelectSpawnPoint();
        StartCoroutine(StartBoostAferDelay());
	}
	

    IEnumerator StartBoostAferDelay()
    {
        yield return new WaitForSeconds(timeBeforeBoost);

        speed *= boostMultiplier;
        boostParticles.Play(false);
    }
	// Update is called once per frame
	void Update () {
        FallingMove();
	}
}
