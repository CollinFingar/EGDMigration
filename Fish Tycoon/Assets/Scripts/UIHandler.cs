using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BoatInfo{
	//If the boat is purchased and active
	public bool active;
	//The current capacity filled on the ship
	public int currentCapacity;
	//The total amount a boat can hold
	public int capactiyCap;
	//The current repair needed 100=None, 0=Totally Broken
	public int repairValue;
	//The amount of crew needed to sail the boat
	public int crewNeeded;
	//Gas remaining
	public float fuelRemaining;
	//Fuel depletion rate per second of moving
	public float fuelDepletionRate;
	//The boat gameobject
	public GameObject boatObject;
}

public struct DockInfo{
	//If the dock is purchased and active
	public bool active;
	//The dock gameobject
	public GameObject dockObject;
}

public class UIHandler : MonoBehaviour {

	//Boat Info
	public BoatInfo[] boats = new BoatInfo[3];
	//Dock Info
	public DockInfo[] docks = new DockInfo[3];


	//REFERENCES=========================
	//Business UI References---------
		//Costs
	public Text fundsAmount;
	public Text dailyCostAmount;
		//Docks
	public Text docksCount;
		//Crew
	public Text crewCount;
		//Boats
	public Image boat1StateColor;
	public Text boat1StateText;
	public Image boat2StateColor;
	public Text boat2StateText;
	public Image boat3StateColor;
	public Text boat3StateText;
	//News UI References-------------
		//Refugees
	public Text savedCount;
	public Text deathCount;
	//Manager UI References----------
	public GameObject managerUI;
		//Boat Options
	public Image boat1StateColorM;
	public Image boat2StateColorM;
	public Image boat3StateColorM;
	public Button boat2Buy;
	public Button boat3Buy;
	public Button boat1Repair;
	public Button boat2Repair;
	public Button boat3Repair;
	public Button boat2Sell;
	public Button boat3Sell;
		//Dock Options
	public Image dock1StateColorM;
	public Image dock2StateColorM;
	public Image dock3StateColorM;
	public Button dock2Buy;
	public Button dock3Buy;
	public Button dock2Sell;
	public Button dock3Sell;
		//Crew Options
	public Button addCrew;
	public Button subtractCrew;
	public Text crewAmount;
	public Text unassignedCrewAmount;
	//END REFERENCES==========================

	public int totalCrew = 10;
	public int unassignedCrew = 0;
	public int funds = 100;
	public int dailyCost = 10;
	public int docksOwned = 1;

	public int refugeesSaved = 0;
	public int refugeesDied = 0;

	private int boatCapactiy = 20;
	private int crewNeeded = 10;

	public Color dockActiveColor;


	// Use this for initialization
	void Start () {
		managerUI.SetActive (false);
		dock1StateColorM.color = dockActiveColor;
		dock2StateColorM.color = Color.gray;
		dock3StateColorM.color = Color.gray;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public void Initialize(GameObject[] boatObjects, GameObject[] dockObjects){
		for (int i = 0; i < 3; i++) {
			boats [i].boatObject = boatObjects [i];
			boats [i].currentCapacity = 0;
			boats [i].capactiyCap = boatCapactiy;
			boats [i].repairValue = 100;
			boats [i].crewNeeded = crewNeeded;
			boats [i].fuelRemaining = 100;
			boats [i].fuelDepletionRate = 1;
			docks [i].dockObject = dockObjects [i];
			if (i == 0) {
				boats [i].active = true;
				docks [i].active = true;
			} else {
				boats [i].active = false;
				docks [i].active = false;
			}
		}
	}


	public void ActivateManagerUI(bool active){
		managerUI.SetActive (active);
		if (active) {
			Time.timeScale = 0;
			RefreshManagerUIButtons ();
		} else {
			Time.timeScale = 1;
		}
	}

	public void RefreshManagerUIButtons(){
		if (boats [1].active) {
			boat2Sell.gameObject.SetActive (true);
			boat2Buy.gameObject.SetActive (false);
			boat2Repair.gameObject.SetActive (true);
		} else {
			boat2Sell.gameObject.SetActive (false);
			boat2Buy.gameObject.SetActive (true);
			boat2Repair.gameObject.SetActive (false);
		}
		if (boats [2].active) {
			boat3Sell.gameObject.SetActive (true);
			boat3Buy.gameObject.SetActive (false);
			boat3Repair.gameObject.SetActive (true);
		} else {
			boat3Sell.gameObject.SetActive (false);
			boat3Buy.gameObject.SetActive (true);
			boat3Repair.gameObject.SetActive (false);
		}
		if (docks [1].active) {
			dock2Sell.gameObject.SetActive (true);
			dock2Buy.gameObject.SetActive (false);
		} else {
			dock2Sell.gameObject.SetActive (false);
			dock2Buy.gameObject.SetActive (true);
		}
		if (docks [2].active) {
			dock3Sell.gameObject.SetActive (true);
			dock3Buy.gameObject.SetActive (false);
		} else {
			dock3Sell.gameObject.SetActive (false);
			dock3Buy.gameObject.SetActive (true);
		}
	}

	public void UpdateRepair(int boatIndex, int value){
		if (boatIndex == 0) {
			UpdateRepairColor (boat1StateColor, value, boats [0].active);
			UpdateRepairColor (boat1StateColorM, value, boats [0].active);
			UpdateRepairText (boat1StateText, value, boats [0].active);
		} else if (boatIndex == 1) {
			UpdateRepairColor (boat2StateColor, value, boats [1].active);
			UpdateRepairColor (boat2StateColorM, value, boats [1].active);
			UpdateRepairText (boat2StateText, value, boats [1].active);
		} else {
			UpdateRepairColor (boat3StateColor, value, boats [2].active);
			UpdateRepairColor (boat3StateColorM, value, boats [2].active);
			UpdateRepairText (boat3StateText, value, boats [2].active);
		}
	}

	//green->value=100
	//yellow->value=50
	//red->value=0
	void UpdateRepairColor(Image status, int value, bool active){
		if (!active) {
			status.color = Color.gray;
			return;
		}
		Color top;
		Color bottom;
		float valuef = value * 1f;
		if (value < 50) {
			top = Color.yellow;
			bottom = Color.red;
		} else {
			top = Color.green;
			bottom = Color.yellow;
			valuef -= 50f;
		}
		valuef /= 50f;
		status.color = Color.Lerp (bottom, top, valuef);
	}
	void UpdateRepairText(Text status, int value, bool active){
		if (!active) {
			status.text = "";
			return;
		}
		status.text = value.ToString ();
	}

	void UpdateRefugeeTexts(){
		savedCount.text = refugeesSaved.ToString ();
		deathCount.text = refugeesDied.ToString ();
	}

	public void UpdateFunds(int newValue){
		funds = newValue;
		UpdateFundsText ();
	}
	void UpdateFundsText(){
		fundsAmount.text = funds.ToString ();
	}

	void UpdateDailyCosts(){
		//TODO: CALCULATE
		UpdateDailyCostsText();
	}
	void UpdateDailyCostsText(){
		dailyCostAmount.text = dailyCost.ToString ();
	}

	public void AddCrew(){
		totalCrew++;
		unassignedCrew++;
		UpdateCrewTexts ();
	}
	public void SubtractCrew(){
		if (unassignedCrew > 0) {
			totalCrew--;
			unassignedCrew--;
			UpdateCrewTexts ();
		} else {
			print ("ERROR: NO UNASSIGNED CREW");
		}
	}

	public void UpdateCrewTexts(){
		crewCount.text = totalCrew.ToString ();
		crewAmount.text = totalCrew.ToString ();
		unassignedCrewAmount.text = unassignedCrew.ToString ();
	}

	public void UpdateBoatState(int boatIndex, bool active){
		boats [boatIndex].active = active;
		RefreshManagerUIButtons ();
	}

	public void UpdateDockState(int dockIndex, bool active){
		docks [dockIndex].active = active;
		UpdateDockColor (dockIndex);
		RefreshManagerUIButtons ();
		if (active) {
			docksOwned++;
			docksCount.text = docksOwned.ToString ();
		} else {
			docksOwned--;
			docksCount.text = docksOwned.ToString ();
		}
	}

	public void UpdateDockColor(int dockIndex){
		if (dockIndex == 1) {
			if (docks [1].active) {
				dock2StateColorM.color = dockActiveColor;
			} else {
				dock2StateColorM.color = Color.gray;
			}
		} else if (dockIndex == 2) {
			if (docks [2].active) {
				dock3StateColorM.color = dockActiveColor;
			} else {
				dock3StateColorM.color = Color.gray;
			}
		}
	}
}
