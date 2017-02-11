using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvents : MonoBehaviour {
	public string[] beforeRefs; //before refugees spawn text
	public string[] refSpawn; //when refugees spawn (still neutral text)
	public string[] lowDeaths; //low death count
	public string[] highDeaths; //high death count
	public string[] highProfits; //when profits are high
	public string[] lowProfits; //when profits are low
	public string[] newDock; //when the player buys a new dock

	List<GameObject> messageQueue; //list of messages to come
	public GameObject messageRef;
	ArrayList textCrawling; //global 
	// Use this for initialization
	void Awake() {
		textCrawling = new ArrayList ();
		messageQueue = new List<GameObject> ();

		ArrayList grouping1 = new ArrayList ();
		ArrayList grouping2 = new ArrayList ();
		ArrayList grouping3 = new ArrayList ();
		ArrayList grouping4 = new ArrayList ();
		ArrayList grouping5 = new ArrayList ();
		ArrayList grouping6 = new ArrayList ();
		ArrayList grouping7 = new ArrayList ();
		int ref1 = 0;
		int ref2 = 0;
		int ref3 = 0;
		int ref4 = 0;
		int ref5 = 0;
		int ref6 = 0;
		int ref7 = 0;

		grouping1.Add (beforeRefs);
		grouping1.Add (ref1);
		grouping1.Add (Color.white);
		textCrawling.Add (grouping1);

		grouping2.Add (refSpawn);
		grouping2.Add (ref2);
		grouping2.Add (Color.white);
		textCrawling.Add (grouping2);

		grouping3.Add (lowDeaths);
		grouping3.Add (ref3);
		grouping3.Add (Color.green);
		textCrawling.Add (grouping3);

		grouping4.Add (highDeaths);
		grouping4.Add (ref4);
		grouping4.Add (Color.red);
		textCrawling.Add (grouping4);

		grouping5.Add (highProfits);
		grouping5.Add (ref5);
		grouping5.Add (Color.green);
		textCrawling.Add (grouping5);

		grouping6.Add (lowProfits);
		grouping6.Add (ref6);
		grouping6.Add (Color.red);
		textCrawling.Add (grouping6);

		grouping7.Add (newDock);
		grouping7.Add (ref7);
		grouping7.Add (Color.white);
		textCrawling.Add (grouping7);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			addMessage (0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			addMessage (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			addMessage (2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			addMessage (3);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			addMessage (4);
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			addMessage (5);
		}
		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			addMessage (6);
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			print(messageQueue);
		}
		if (messageQueue.Count > 0 && messageQueue[0] == null) {
			messageQueue.RemoveAt (0);
			if (messageQueue.Count > 0) {
				messageQueue [0].GetComponent<TextCrawl> ().ScrollText ();
			}
		}
	}

	public void addMessage(int type) {
		GameObject newMessage = (GameObject)Instantiate (messageRef,transform);
		Text textRef = newMessage.GetComponent<Text> ();
		textRef.color = (Color)((ArrayList)textCrawling [type])[2]; //gets color of message (white = neutral, green = good, red = bad)
		int refInt = (int)((ArrayList)textCrawling [type]) [1];
		string[] mesRef = (string[])((ArrayList)textCrawling [type]) [0];
		textRef.text = mesRef [refInt];
		if (refInt + 1 >= mesRef.Length) {
			((ArrayList)textCrawling [type])[1] = 0; 
		} else {
			((ArrayList)textCrawling [type]) [1] = (int)((ArrayList)textCrawling [type])[1] + 1;
		}
		TextCrawl crawlRef = newMessage.GetComponent<TextCrawl>();
		crawlRef.TextSpawn (mesRef [refInt], (Color)((ArrayList)textCrawling [type]) [2]);
		textRef.enabled = false;
		if (messageQueue.Count == 0) {
			crawlRef.ScrollText ();
		}
		messageQueue.Add (newMessage);
		//textCrawling[type]
	}
}
