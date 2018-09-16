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

    void OnEnable()
    {
        myText = GetComponent<Text>();
        if (myText == null)
        {
            Debug.LogAssertion("The Lives display script must be attached to a UI Text object");
        }
        LivesManager.OnLifeAdded += UpdateLives;
        LivesManager.OnLifeSubtracted += UpdateLives;

    }

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        LivesManager.OnLifeAdded -= UpdateLives;
        LivesManager.OnLifeSubtracted -= UpdateLives;
    }
    private void OnDestroy()
    {
        LivesManager.OnLifeAdded -= UpdateLives;
        LivesManager.OnLifeSubtracted -= UpdateLives;
    }
}
