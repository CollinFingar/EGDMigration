using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Update all the info about this boat
    public void UpdateBoatInfo (int InMaxHP, int InCurrentHP, int InMaxCP, int InCurrentCP)
    {
        MaxHP = InMaxHP;
        CurrentHP = InCurrentHP;
        MaxCP = InMaxCP;
        CurrentCP = InCurrentCP;
        HPRatio.text = string.Format("{0} / {1}", CurrentHP, MaxHP);
        CapacityRatio.text = string.Format("{0} / {1}", CurrentCP, MaxCP);
    }

    private int MaxHP;
    private int CurrentHP;
    private int MaxCP;
    private int CurrentCP;
    public Text HPRatio;
    public Text CapacityRatio;
}
