using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {
	public string gameScene;
	public string endScene;
	public string menuScene;

	// Use this for initialization
	void Start () {
		
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
