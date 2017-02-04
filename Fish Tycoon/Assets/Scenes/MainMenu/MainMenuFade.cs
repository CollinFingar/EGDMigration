using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
        bFading = false;
        CurrentVelocity = InitialVelocity;
    }
	
	// Update is called once per frame
	void Update () {
        if (bFading)
        {
            Vector3 Offset = CalculateSpeed();
            TitleImage.transform.Translate(Offset);
            StartButton.transform.Translate(Offset);
            ExitButton.transform.Translate(Offset);

            if (TitleImage.transform.position.x < -10.0f )
            {
                bFading = false;
            }
        }
    }

    public void Fade()
    {
        bFading = true;
        TitleImage.GetComponent<Image>().CrossFadeAlpha(0.01f, 1.0f, false);
        StartButton.GetComponent<Image>().CrossFadeAlpha(0.01f, 1.0f, false);
        ExitButton.GetComponent<Image>().CrossFadeAlpha(0.01f, 1.0f, false);

        StartButton.GetComponentInChildren<Text>().CrossFadeAlpha(0.01f, 1.0f, false);
        ExitButton.GetComponentInChildren<Text>().CrossFadeAlpha(0.01f, 1.0f, false);
    }

    private Vector3 CalculateSpeed()
    {
        CurrentVelocity += Acceleration;
        return CurrentVelocity;
    }

    public GameObject TitleImage;
    public GameObject StartButton;
    public GameObject ExitButton;

    public Vector3 InitialVelocity = new Vector3(5f, 0, 0);
    public Vector3 Acceleration = new Vector3(-0.35f, 0, 0);

    private Vector3 CurrentVelocity;

    private bool bFading;
}
