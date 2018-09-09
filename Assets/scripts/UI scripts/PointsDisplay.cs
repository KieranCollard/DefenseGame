using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour {

    Text myText;
    public string precedingText = "Score: ";
    void UpdateScore(int currentScore)
    {
        myText.text = precedingText + currentScore.ToString();
    }

    private void OnEnable()
    {
        PointsManager.OnScoreAdded += UpdateScore;
        PointsManager.OnScoreSubtracted += UpdateScore;
    }
    // Use this for initialization
    void Start () {
        myText = GetComponent<Text>();
        if(myText == null)
        {
            Debug.LogAssertion("The Points display script must be attached to a UI text object");
        }
	}

    private void OnDisable()
    {
        PointsManager.OnScoreSubtracted -= UpdateScore;
        PointsManager.OnScoreAdded -= UpdateScore;
    }

    private void OnDestroy()
    {
        PointsManager.OnScoreSubtracted -= UpdateScore;
        PointsManager.OnScoreAdded -= UpdateScore;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
