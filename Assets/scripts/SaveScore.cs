using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveScore : MonoBehaviour {

    public GameObject HighScoreSystem;
    public GameObject textInput;
    public GameObject GameManager;

    HighScoreSystem highScoreSystem;
    InputField input;
    LoadPostGame loadPost;

    public void Start()
    {

        if(HighScoreSystem == null)
        {
            Debug.LogAssertion("The savescore script did not have a HighScoreSystem referenced");
        }
        if(textInput == null)
        {
            Debug.LogAssertion("The savescore script did not have the input field reference");
        }
        if(GameManager == null)
        {
            Debug.LogAssertion("The game manager reference was not valid");
        }

        highScoreSystem = HighScoreSystem.GetComponentInChildren<HighScoreSystem>();
        input = textInput.GetComponentInChildren<InputField>();
        loadPost = GameManager.GetComponentInChildren<LoadPostGame>();

        if(loadPost == null)
        {
            Debug.LogAssertion("The game manager did not have a load post game script");
        }
        if(highScoreSystem == null)
        {
            Debug.LogAssertion("The high score system reference did not have a high score system script");
        }
        if(input == null)
        {
            Debug.LogAssertion("The text input object did not have an input field " + this.gameObject.ToString()) ;
        }
    }
    public void SaveDataAndLoadPostGame()
    {
        highScoreSystem.AddValue(input.text, PointsManager.CurrentScore);
        highScoreSystem.Save();
        loadPost.LoadPostGameScene();
    }
}
