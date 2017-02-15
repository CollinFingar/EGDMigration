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
	public int boatDailyCost = 10;

	public int dockBuyPrice = 100;
	public int dockSellPrice = 50;
	public int dockDailyCost = 10;

	public int totalCrewCount = 5;
	public int unassignedCrewCount = 0;
	public int crewDailyCost = 2;

	int dockMax = 1; //maximum number of docks that have been owned (prevents buy and sell prompt spam)

	float promptTimer = 0; //time since last prompt was displayed
	public float messageFreq = 60; //frequency of message (in seconds)

	//the following variables are comparisson variables to see how the player is doing and send messages accordingly
	int messageNum; //which message to send, cycles overtime
	float timedFunds;
	float timedSaves; //refugees saved since last update
	float timedDeaths; //refugees that died since last update

	//sound effects associated with this manager
	public AudioSource chaChing;


	// Use this for initialization
	void Start () {
		boats [0] = boat1Object;
		boats [1] = boat2Object;
		boats [2] = boat3Object;
        Initialize ();
		timedFunds = funds;
		timedSaves = 0;
		timedDeaths = 0;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 3; i++) {
			UpdateRepair (boats [i], 100);
		}
		UI.UpdateFunds (funds);
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
        if (funds <= 0)
        {
            Application.Quit();
        }
		promptTimer += Time.deltaTime;
		if (promptTimer > messageFreq) {
			GenerateMessage ();
			promptTimer = 0;
		}
	}

	void Initialize(){
		UI.Initialize (boats, docks);
		boats [1].SetActive (false);
		boats [2].SetActive (false);
		docks [1].SetActive (false);
		docks [2].SetActive (false);
		UpdateCosts ();
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
			unassignedCrewCount -= 5;
			UI.unassignedCrew -= 5;
			UI.UpdateCrewTexts ();
			UpdateCosts ();
		//Becoming inactive
		} else if (!active && activeState) {
			boat.SetActive(false);
			UI.UpdateBoatState (boatIndex, false);
			UI.UpdateRepair (boatIndex, 100);
			unassignedCrewCount += 5;
			UI.unassignedCrew += 5;
			UI.UpdateCrewTexts ();
			UpdateCosts ();
		}
	}

	public void BuyBoat(int boatIndex){
		if (funds >= boatBuyPrice && unassignedCrewCount >= 5) {
			funds -= boatBuyPrice;
			activateBoat (boatIndex, true);
			chaChing.Play ();
			UpdateCosts ();
		}
	}

	public void SellBoat(int boatIndex){
		funds += boatSellPrice;
		activateBoat (boatIndex, false);
		UpdateCosts ();
	}

	void activateDock(int dockIndex, bool active){
		GameObject dock = docks [dockIndex];
		bool activeState = dock.activeSelf;
		//Becoming active
		if (active && !activeState) {
			dock.SetActive(true);
			UI.UpdateDockState (dockIndex, true);
			UpdateCosts ();
			//Becoming inactive
		} else if (!active && activeState) {
			dock.SetActive(false);
			UI.UpdateDockState (dockIndex, false);
			UpdateCosts ();
		}
	}

	public void BuyDock(int dockIndex){
		if (funds >= dockBuyPrice) {
			funds -= dockBuyPrice;
			activateDock (dockIndex, true);
			chaChing.Play ();
			print (dockIndex);
			if (dockMax <= dockIndex) {
				GameObject.Find ("TextCrawlCanvas").GetComponent<GameEvents> ().messageQueue.Add (6);
				dockMax++;
			}
			UpdateCosts ();
		}
	}

	public void SellDock(int dockIndex){
		funds += dockSellPrice;
		activateDock (dockIndex, false);
		UpdateCosts ();
	}

	public void AddCrew(){
		totalCrewCount++;
		unassignedCrewCount++;
		UI.AddCrew ();
		UpdateCosts ();
	}

	public void SubtractCrew(){
		if (unassignedCrewCount > 0) {
			totalCrewCount--;
			unassignedCrewCount--;
			UI.SubtractCrew ();
			UpdateCosts ();
		}
	}

	public void UpdateCosts(){
		int dockCount = 0;
		for (int i = 0; i < docks.Length; i++) {
			if (docks [i].activeSelf) {
				dockCount++;
			}
		}
		int boatCount = 0;
		for (int i = 0; i < boats.Length; i++) {
			if (boats [i].activeSelf) {
				boatCount++;
			}
		}
		int crewCost = totalCrewCount * crewDailyCost;
		int dockCost = dockCount * dockDailyCost;
		int boatCost = boatCount * boatDailyCost;
        dailyCost = crewCost + dockCost + boatCost;
		UI.UpdateCosts (dockCost, crewCost, boatCost);
	}

	void GenerateMessage() {
		GameEvents bottomText = GameObject.Find ("TextCrawlCanvas").GetComponent<GameEvents> ();
		if (messageNum == 0) {
			bottomText.messageQueue.Add (1);
		}
		if (messageNum == 1) {
			if ((refugeesSaved - timedSaves) / (refugeesDied - timedDeaths) < 0.5f) {
				bottomText.messageQueue.Add (3);
			} else {
				bottomText.messageQueue.Add (2);
			}
		} else if (messageNum == 2) {
			if (timedFunds > funds * 1.1f) {
				bottomText.messageQueue.Add (4);
			} else {
				bottomText.messageQueue.Add (5);
			}
		}
		messageNum++;
		if (messageNum > 2) {messageNum = 0;}
		timedSaves = refugeesSaved;
		timedDeaths = refugeesDied;
		timedFunds = funds;
	}
}
