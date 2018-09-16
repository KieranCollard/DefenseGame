using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDisplayParent : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        LivesManager.GameOverEvent += OnGameOver;
        
        foreach(Transform child in GetComponentInChildren<Transform>(true))
        {
            child.gameObject.SetActive(false);
        }
	}

    private void OnDestroy()
    {
        LivesManager.GameOverEvent -= OnGameOver;
    }

    void OnGameOver()
    {
        //enable children
        foreach (Transform child in GetComponentInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true);
        }
    }
}
