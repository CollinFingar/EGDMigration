using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testClockScript : MonoBehaviour {

	LevelHandler level;

	// Use this for initialization
	void Start () {
		level = LevelHandler.Instance;
		this.GetComponentInParent<Text> ().text = level.getClock ().ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponentInParent<Text> ().text = level.getClock ().ToString ();
	}
}
