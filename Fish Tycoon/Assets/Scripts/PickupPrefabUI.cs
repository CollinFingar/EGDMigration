using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPrefabUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UITransform.SetActive(false);
        if(IsFish)
        {
            Human.SetActive(false);
            Fish.SetActive(true);
        }
        else
        {
            Fish.SetActive(false);
            Human.SetActive(true);
        }
	}
	
    public void InitializeParameters(bool IsFish, int Amount, int AffordableAmount)
    {
        this.IsFish = IsFish;
        this.Amount = Amount;
        UI.InitializeParameters(this.IsFish, this.Amount, AffordableAmount);
    }

    public void ShowUI(bool bShouldShow)
    {
        UITransform.SetActive(bShouldShow);
    }

	// Update is called once per frame
	void Update () {
		
	}

    public PickUpUI UI;
    public GameObject UITransform;
    public bool IsFish;
    public int Amount;
    public GameObject Fish;
    public GameObject Human;

}
