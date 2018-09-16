using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneraateTextObjects : MonoBehaviour {
    public GameObject HighScoreReference;
    HighScoreSystem highScore;
	// Use this for initialization
	void Start () {
		if(HighScoreReference == null)
        {
            Debug.LogAssertion("The highScore object was not provided");
        }

        HighScoreReference =  Instantiate(HighScoreReference);
        highScore = HighScoreReference.GetComponent<HighScoreSystem>();

        if(highScore == null)
        {
            Debug.LogAssertion("High score script was missing from highscore prefab");

        }

        highScore.Load();

        foreach(HighScoreSystem.NameToScore nameToScore in highScore.highScoreList)
        {
            GameObject newObject = new GameObject();
            Text textComponent = newObject.AddComponent<Text>();
            newObject.transform.SetParent(this.transform);
            textComponent.text = nameToScore.Name;

            GameObject scoreObject = new GameObject();
            Text scoreComponent = scoreObject.AddComponent<Text>();
            scoreObject.transform.SetParent(this.transform);
            scoreComponent.text = nameToScore.scoreValue.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
