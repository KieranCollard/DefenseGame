using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LivesManager : MonoBehaviour {

    public delegate void OnLifeAddedDelegate(int currentLivesCount);
    public static OnLifeAddedDelegate OnLifeAdded;

    public delegate void OnLifeSubtractedDelegate(int currentLivesCount);
    public static OnLifeSubtractedDelegate OnLifeSubtracted;

    public delegate void AddLifeDelegate();
    public static AddLifeDelegate AddLifeEvent;

    public delegate void RemoveLifeDelegate();
    public static RemoveLifeDelegate RemoveLifeEvent;

    public delegate void GameOverDelegate();
    public static GameOverDelegate GameOverEvent;

    public int startingLives = 3;
    private int currentLives;

    public int CurrentLives
    {
        get { return currentLives; }
    }

    public void AddLife()
    {
        currentLives += 1;
        if(OnLifeAdded != null)
            OnLifeAdded(currentLives);
    }

    public void SubtractLife()
    {
        currentLives -= 1;

        if(currentLives <= 0)
        {
            if (GameOverEvent != null)
            {
                Time.timeScale = 0;
                GameOverEvent();
            }
            else
            {
                Debug.LogAssertion("Nothing was subscribed to the game over event. The game cannot end");
            }
        }
        if(OnLifeAdded != null)
            OnLifeSubtracted(currentLives);
    }
	// Use this for initialization
	void Start () {
        currentLives = startingLives;

        AddLifeEvent += AddLife;
        RemoveLifeEvent += SubtractLife;

        //unpause if we were paused
        Time.timeScale = 1;
        if(OnLifeAdded != null)
            OnLifeAdded(currentLives);
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
