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

	// Use this for initialization
	void Start () {
		spawnNodes = new GameObject[spawnNodeFolder.transform.childCount];
		int i = 0;
		foreach (Transform child in spawnNodeFolder.transform) {
			spawnNodes [i] = child.gameObject;
			i++;
			freeNodes.Add (i);
		}
		numSpawnNodes = spawnNodeFolder.transform.childCount;
		SpawnRefugee ();
		SpawnFish ();
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
}
