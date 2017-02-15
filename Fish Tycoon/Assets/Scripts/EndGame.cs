using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {
	public GameObject gameState; //object that has relevant info from game scene
	public Text endText; //text box above the word Afloat that is impacted by end conditions

	public Color winBackColor; 
	public Color loseBackColor; 
	public Text refsSavedText;
	public Text refsDiedText;
	public Text fundsText;
	public bool goodGame;
	// Use this for initialization
	void Start () {
		

		if (goodGame) {
			endText.text = "Your company managed to stay...";
			GetComponent<Camera> ().backgroundColor = winBackColor;
		} else {
			endText.text = "Your company did not manage to stay...";
			GetComponent<Camera> ().backgroundColor = loseBackColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
