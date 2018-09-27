using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBaseObject : MonoBehaviour {

    public int pointScore = 0;
    public float speed = 1;

    public Transform bulletReference;
    public GameObject groundReference;

    public GameObject ExplosionParticles = null;
    ParticleSystem particles = null;

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

        if(ExplosionParticles == null)
        {
            Debug.LogAssertion("THe missile base object did not have an explosion particles object assigned");
        }
        particles = ExplosionParticles.GetComponentInChildren<ParticleSystem>();
        if (particles == null )
        {
            Debug.LogAssertion("The assigned explosion particles did not have a particle effect");
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShotFromSky()
    {
        StartCoroutine(DestroyBehaviour()); 
        if(PointsManager.AddScoreEvent != null)
            PointsManager.AddScoreEvent(pointScore);
    }

    public void MadeItToGround()
    {
        StartCoroutine(DestroyBehaviour());   
        if (LivesManager.RemoveLifeEvent != null)
            LivesManager.RemoveLifeEvent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //8 being our defined bullet layer
        if (collision.gameObject.layer == 8)
        {
            ShotFromSky();
        }
        //10 being our defined ground layer
        else if (collision.gameObject.layer == 10)
        {
            MadeItToGround();
        }
    }

    IEnumerator DestroyBehaviour()
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source == null)
        {
            Debug.LogAssertion("The missile object " + this.gameObject + "did not have an audio source to play sound effects");

        }
        else
        {
            source.PlayOneShot(source.clip);
        }
        Instantiate(ExplosionParticles, this.transform.position, ExplosionParticles.transform.rotation);
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        ParticleSystem particles = this.GetComponent<ParticleSystem>();
        if (particles != null)
            particles.Stop();

        yield return new WaitForSeconds(source.clip.length);
        Destroy(this.gameObject);
    }
}
