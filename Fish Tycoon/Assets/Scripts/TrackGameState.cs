using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackGameState : MonoBehaviour {
	public int refSaved;
	public int refDied;
	public int funds;

	public string gameScene;
	public string endScene;
	public string menuScene;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void StartGame () {
		SceneManager.LoadScene (gameScene);
	}

	public void EndGame () {
		SceneManager.LoadScene (endScene);
	}

	public void RestartGame() {
		SceneManager.LoadScene (menuScene);
	}
		


}
