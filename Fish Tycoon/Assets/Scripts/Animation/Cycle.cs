using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour {

	public float speedOfCycle = .5f;

	public GameObject[] images;

	public int start = 0;
	private int index = 0;

	private float nextSwitchTime = 0f;

	// Use this for initialization
	void Start () {
		index = start;
		for (int i = 0; i < images.Length; i++) {
			if (i == index) {
				images [i].SetActive (true);
			} else {
				images [i].SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSwitchTime) {
			int next = index+1;
			if (next == images.Length) {
				next = 0;
			}
			UpdateFrame (index, next);
			index = next;
			nextSwitchTime = Time.time + speedOfCycle;
		}

	}

	void UpdateFrame(int current, int next){
		images [current].SetActive (false);
		images [next].SetActive (true);
	}
}
