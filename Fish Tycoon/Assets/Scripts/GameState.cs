using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
	public int refsSaved;
	public int refsDied;
	public int funds;
	public bool transitioned;
	// Use this for initialization
	void Start () {
		transitioned = false;
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnLevelWasLoaded() {
		if (transitioned) {
			Destroy (this.gameObject);
		} else {
			transitioned = true;
			SetEndText ();
		}
	}

	void SetEndText() {
		GameObject.Find ("RefsSavedText").GetComponent<Text> ().text += refsSaved;
		GameObject.Find ("RefsDiedText").GetComponent<Text> ().text += refsDied;
		GameObject.Find ("FundsText").GetComponent<Text> ().text += funds;
	}
}
