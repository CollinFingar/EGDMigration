using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPrefabScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UITransform.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void UpdateBoatInfo(int InMaxHP, int InCurrentHP, int InMaxCP, int InCurrentCP)
    {
        MaxHP = InMaxHP;
        CurrentHP = InCurrentHP;
        MaxCP = InMaxCP;
        CurrentCP = InCurrentCP;
        UI.UpdateBoatInfo(MaxCP, CurrentHP, MaxCP, CurrentCP);
    }

    public void ShowUI (bool bShouldShow)
    {
        UITransform.SetActive(bShouldShow);
    }

    public GameObject Boat;
    public BoatUI UI;
    public GameObject UITransform;
    public int MaxHP;
    public int CurrentHP;
    public int MaxCP;
    public int CurrentCP;
}
