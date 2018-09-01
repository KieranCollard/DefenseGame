using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour {


    Text myText;
    public string precedingText = "Lives: ";
    void UpdateLives(int currentLives)
    {
        myText.text = precedingText + currentLives.ToString();
    }
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        if(myText == null)
        {
            Debug.LogAssertion("The Lives display script must be attached to a UI Text object");
        }

        LivesManager.OnLifeAdded += UpdateLives;
        LivesManager.OnLifeSubtracted += UpdateLives;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        LivesManager.OnLifeAdded -= UpdateLives;
        LivesManager.OnLifeSubtracted -= UpdateLives;
    }
}
