using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePointsDisplay : MonoBehaviour {

    public string suffixString = "Points!";

    Text displayText;

    //assumed that our object is being disabled / enabled by it's parent
    private void OnEnable()
    {

        displayText = this.GetComponent<Text>();

        if (displayText == null)
        {
            Debug.LogAssertion("The end game points display object was not attached to a UI text");
        }

        displayText.text = PointsManager.CurrentScore.ToString() + " " + suffixString;
    }

}
