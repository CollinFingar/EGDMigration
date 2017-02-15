using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {

	public GameObject fishObject;
	public GameObject refugee;

	public GameObject spawnNodeFolder;
	public GameObject[] spawnNodes;
	public ArrayList freeNodes = new ArrayList();

	private int numSpawnNodes = 0;

	private float time = 0;

	public float fishLife = 20f;
	public float refugeeLife = 10f;

	private float fishSpawnDelay = 18f;
	private float refugeeSpawnDelay = 16f; 

	private float fishNextSpawn = 0f;
	private float refugeeNextSpawn = 0f;

	private int[] fishDayLevels = { 1, 2, 2, 2, 3, 3, 3, 3, 3, 4 };
	private int[] refugeeDayLevels = { 1, 2, 2, 3, 3, 3, 4, 4, 4, 5 };

	private int day = 0;

	// Use this for initialization
	void Start () {
		spawnNodes = new GameObject[spawnNodeFolder.transform.childCount];
		int i = 0;
		foreach (Transform child in spawnNodeFolder.transform) {
			spawnNodes [i] = child.gameObject;

			freeNodes.Add (i);
			i++;
		}
		numSpawnNodes = spawnNodeFolder.transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
	}

	void SpawnRefugee(){
		int r = Random.Range (0, freeNodes.Count-1);
		int freeSpawnNodeIndex = (int) freeNodes [r];
		//GIVE REFUGEE NODE INDEX
		GameObject node = spawnNodes[freeSpawnNodeIndex];
		GameObject newRefugeee = (GameObject)Instantiate (refugee, node.transform.position, Quaternion.identity);
		freeNodes.RemoveAt (r);
		AssignIndex (newRefugeee, freeSpawnNodeIndex);
	}

	void SpawnFish(){
		int r = Random.Range (0, freeNodes.Count-1);
		int freeSpawnNodeIndex = (int) freeNodes [r];
		//GIVE FISH NODE INDEX
		GameObject node = spawnNodes[freeSpawnNodeIndex];
		GameObject newFish = (GameObject)Instantiate (fishObject, node.transform.position, Quaternion.identity);
		freeNodes.RemoveAt (r);
		AssignIndex (newFish, freeSpawnNodeIndex);
	}

	void AssignIndex(GameObject obj, int index){
		obj.GetComponent<DeathPassSpawn> ().spawnNodeIndex = index;
		obj.GetComponent<DeathPassSpawn> ().SH = this.gameObject.GetComponent<SpawnHandler>();
	}

	public void ReturnIndex(int index){
		freeNodes.Add (index);
	}

	//1-10
	public void NewWave(int d){
		day = d;
		DeathPassSpawn[] spawnObjects = FindObjectsOfType<DeathPassSpawn> ();
		for (int i = 0; i < spawnObjects.Length; i++) {
			Destroy (spawnObjects [i].gameObject);
		}
		int fishToSpawn = fishDayLevels [day - 1];
		int refugeesToSpawn = refugeeDayLevels [day - 1];
		print ("FISH SPAWNING: " + fishToSpawn.ToString () + " REFUGEES: " + refugeesToSpawn.ToString ());
		for (int i = 0; i < fishToSpawn; i++) {
			SpawnFish ();
		}
		for (int i = 0; i < refugeesToSpawn; i++) {
			SpawnRefugee ();
		}
	}
}
