using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	public UIHandler UI;

	public GameObject boat1Object;
	public GameObject boat2Object;
	public GameObject boat3Object;
	private GameObject[] boats = new GameObject[3];

	public int funds = 0;
	public int dailyCost = 0;

	public int refugeesSaved = 0;
	public int refugeesDied = 0;


	// Use this for initialization
	void Start () {
		boats [0] = boat1Object;
		boats [1] = boat2Object;
		boats [2] = boat3Object;
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 3; i++) {
			UpdateRepair (boats [i], 100);
		}
	}

	void Initialize(){
		UI.Initialize (boats);
		boats [1].SetActive (false);
		boats [2].SetActive (false);
	}

	public void UpdateRepair(GameObject boat, int value){
		for (int i = 0; i < 3; i++) {
			if (boat == boats [i]) {
				UI.UpdateRepair (i, value);
			}
		}
	}

	public void BuyBoat(){}
}
