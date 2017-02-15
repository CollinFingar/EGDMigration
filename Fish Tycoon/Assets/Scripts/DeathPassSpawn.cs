﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPassSpawn : MonoBehaviour {

	public int spawnNodeIndex = 0;

	public SpawnHandler SH;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		SH.ReturnIndex (spawnNodeIndex);
		if (gameObject.tag == "Refugee") {
			int total = GetComponent<CollectibleBehavior> ().total;
			print (total.ToString ());
			FindObjectOfType<UIHandler> ().AddRefugeePerished (total);
		}
	}
}
