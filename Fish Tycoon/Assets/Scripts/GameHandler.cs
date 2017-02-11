using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	public UIHandler UI;

	public GameObject boat1Object;
	public GameObject boat2Object;
	public GameObject boat3Object;
	private GameObject[] boats = new GameObject[3];

	public GameObject[] docks = new GameObject[3];

	public int funds = 0;
	public int dailyCost = 0;

	public int refugeesSaved = 0;
	public int refugeesDied = 0;

	public int boatBuyPrice = 100;
	public int boatSellPrice = 50;

	public int dockBuyPrice = 100;
	public int dockSellPrice = 50;

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
		UI.UpdateFunds (funds);
	}

	void Initialize(){
		UI.Initialize (boats, docks);
		boats [1].SetActive (false);
		boats [2].SetActive (false);
		docks [1].SetActive (false);
		docks [2].SetActive (false);
	}

	public void UpdateRepair(GameObject boat, int value){
		for (int i = 0; i < 3; i++) {
			if (boat == boats [i]) {
				UI.UpdateRepair (i, value);
			}
		}
	}

	void activateBoat(int boatIndex, bool active){
		GameObject boat = boats [boatIndex];
		bool activeState = boat.activeSelf;
		//Becoming active
		if (active && !activeState) {
			boat.transform.position = docks[0].transform.position;
			boat.SetActive(true);
			UI.UpdateBoatState (boatIndex, true);
			UI.UpdateRepair (boatIndex, 100);
		//Becoming inactive
		} else if (!active && activeState) {
			boat.SetActive(false);
			UI.UpdateBoatState (boatIndex, false);
			UI.UpdateRepair (boatIndex, 100);
		}
	}

	public void BuyBoat(int boatIndex){
		if (funds >= boatBuyPrice) {
			funds -= boatBuyPrice;
			activateBoat (boatIndex, true);
		}
	}

	public void SellBoat(int boatIndex){
		funds += boatSellPrice;
		activateBoat (boatIndex, false);
	}

	void activateDock(int dockIndex, bool active){
		GameObject dock = docks [dockIndex];
		bool activeState = dock.activeSelf;
		//Becoming active
		if (active && !activeState) {
			dock.SetActive(true);
			UI.UpdateDockState (dockIndex, true);
			//Becoming inactive
		} else if (!active && activeState) {
			dock.SetActive(false);
			UI.UpdateDockState (dockIndex, false);
		}
	}

	public void BuyDock(int dockIndex){
		if (funds >= dockBuyPrice) {
			funds -= dockBuyPrice;
			activateDock (dockIndex, true);
		}
	}

	public void SellDock(int dockIndex){
		funds += dockSellPrice;
		activateDock (dockIndex, false);
	}
}
