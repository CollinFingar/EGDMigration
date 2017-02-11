using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BoatInfo{
	//If the boat is purchased and active
	public bool active;
	//The level of the boat. The higher, the more capactiy and more crew needed
	int level;
	//The current capacity filled on the ship
	int currentCapacity;
	//The total amount a boat can hold
	int capactiyCap;
	//The current repair needed 100=None, 0=Totally Broken
	int repairValue;
	//The amount of crew needed to sail the boat
	int crewNeeded;
	//Gas remaining
	float fuelRemaining;
	//Fuel depletion rate per second of moving
	float fuelDepletionRate;
	//The boat gameobject
	GameObject boatObject;
}

public struct DockInfo{
	//If the dock is purchased and active
	bool active;
	//Price to purchase dock
	int price;
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
	public Button boat1Buy;
	public Button boat2Buy;
	public Button boat3Buy;
	public Button boat1Repair;
	public Button boat2Repair;
	public Button boat3Repair;
	public Button boat1Sell;
	public Button boat2Sell;
	public Button boat3Sell;
		//Dock Options
	public Image dock1StateColorM;
	public Image dock2StateColorM;
	public Image dock3StateColorM;
	public Button dock1Buy;
	public Button dock2Buy;
	public Button dock3Buy;
	public Button dock1Sell;
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


	// Use this for initialization
	void Start () {
		managerUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	void Initialize(){
		
	}

	void BoatInitializer(BoatInfo infoStruct, bool active){

	}

	public void ActivateManagerUI(bool active){
		managerUI.SetActive (active);
	}

	//green->value=100
	//yellow->value=50
	//red->value=0
	void UpdateRepairColor(Image status, int value){
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
	void UpdateRepairText(Text status, int value){
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

	void UpdateCrewTexts(){
		crewCount.text = totalCrew.ToString ();
		crewAmount.text = totalCrew.ToString ();
		unassignedCrewAmount.text = unassignedCrew.ToString ();
	}
}
