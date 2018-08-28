using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIssileSpawnManager : MonoBehaviour {

    //interval to spawn the missiles at 
    public float spawnInterval = 1;
    //fastest that our interval should get

    public float spawnIntervalMinimum = 0.25f;
    //how often the spaw nreate should increase
    public float speedIncreaseInterval = 1;
    //seconds to increase spawn rate by at each interval
    public float speedIncreaseSeconds = 1;

    public bool spawnActive = true;
    public List<GameObject> knownMissileTypes;
	
    // Use this for initialization
	void Start () {
		if(knownMissileTypes.Count == 0)
        {
            Debug.LogAssertion("The missile spawn manager had no missile types to spawn");
        }
        else
        {
            StartCoroutine(SpawnMissile());
            StartCoroutine(DifficultyTimer());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnMissile()
    {
        while (spawnActive)
        {
            //select a missile type at random
            GameObject tempObj = knownMissileTypes[Random.Range(0, knownMissileTypes.Count)];

            //the assumption is that each missile object will handle it's own placement and start behaviour
            Instantiate(tempObj);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator DifficultyTimer()
    {
        while(spawnActive)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            spawnInterval -= speedIncreaseSeconds;
            if (spawnInterval <= spawnIntervalMinimum)
            {
                spawnInterval = spawnIntervalMinimum;
                
            }
        }
    }
}
