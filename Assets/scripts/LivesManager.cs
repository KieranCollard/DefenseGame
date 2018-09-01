using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour {

    public delegate void OnLifeAddedDelegate(int currentLivesCount);
    public static OnLifeAddedDelegate OnLifeAdded;

    public delegate void OnLifeSubtractedDelegate(int currentLivesCount);
    public static OnLifeSubtractedDelegate OnLifeSubtracted;

    public delegate void AddLifeDelegate();
    public static AddLifeDelegate AddLifeEvent;

    public delegate void RemoveLifeDelegate();
    public static RemoveLifeDelegate RemoveLifeEvent;


    public int startingLives = 3;
    private int currentLives;

    public int CurrentLives
    {
        get { return currentLives; }
    }

    public void AddLife()
    {
        currentLives += 1;
        OnLifeAdded(currentLives);
    }

    public void SubtractLife()
    {
        currentLives -= 1;

        if(currentLives <= 0)
        {
            ///TODO signal game over
        }
        OnLifeSubtracted(currentLives);
    }
	// Use this for initialization
	void Start () {
        currentLives = startingLives;
        OnLifeAdded(currentLives);

        AddLifeEvent += AddLife;
        RemoveLifeEvent += SubtractLife;
    }


    private void OnDestroy()
    {
        AddLifeEvent -= AddLife;
        RemoveLifeEvent -= SubtractLife;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
