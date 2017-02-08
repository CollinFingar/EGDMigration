using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitializeParameters(false, 10, 8);
	}
	
	// Update is called once per frame
	void Update () {
        int CurrentValue = (int)Slider.value;
        int MaxValue = (int)Slider.maxValue;

        TextRatio.text = string.Format("{0} / {1}", CurrentValue, MaxValue);
        AcceptButton.interactable = Slider.value > Affordable ? false : true;
	}

    // Initialize function
    void InitializeParameters(bool IsFish, int Amount, int AffordableAmount)
    {
        this.IsFish = IsFish;
        this.Affordable = AffordableAmount;
        Slider.maxValue = Amount;
        Slider.minValue = 0.0f;
        Slider.value = AffordableAmount;
        Slider.wholeNumbers = true;
        TextRatio.text = string.Format("{0} / {1}", Slider.value, Slider.maxValue);
        IconType.sprite = IsFish ? FishSprite : HumanSprite;
    }

    private bool IsFish; // Otherwise it's refugee
    private int Affordable;
    public Sprite FishSprite;
    public Sprite HumanSprite;
    public Slider Slider;
    public Text TextRatio;
    public Image IconType;
    public Button AcceptButton;
    public Button RefuseButton;
    
}
