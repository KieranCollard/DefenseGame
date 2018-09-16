using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {

    public delegate void OnScoreAddedDelegate(int newCurrentScore);
    public static OnScoreAddedDelegate OnScoreAdded;

    public delegate void OnScoreSubtractedDelegate(int newCurrrentScore);
    public static OnScoreSubtractedDelegate OnScoreSubtracted;

    public delegate void AddScoreDelegate(int scoreValue);
    public static AddScoreDelegate AddScoreEvent;

    public delegate void RemoveScoreDelegate(int scoreValue);
    public static RemoveScoreDelegate RemoveScoreEvent;

    private static int currentScore = 0;

    public static int CurrentScore
    {
        get
        {
            return currentScore;
        }
    }


    public void AddScore(int additionalValue)
    {
        currentScore += additionalValue;
        if(OnScoreAdded != null)
            OnScoreAdded(currentScore);
    }

    public void SubtractScore(int subtractiveValue)
    {
        currentScore -= subtractiveValue;
        OnScoreSubtracted(currentScore);
    }

    private void Start()
    {
        AddScoreEvent += AddScore;
        RemoveScoreEvent += SubtractScore;

        //stimualte the event to update any listeners from their default values
        AddScore(0);
    }

    private void OnDestroy()
    {
        AddScoreEvent -= AddScore;
        RemoveScoreEvent -= SubtractScore;
        //reset score on destruction
        currentScore = 0;
    }
}
