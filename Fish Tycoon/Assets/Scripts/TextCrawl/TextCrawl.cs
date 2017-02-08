using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCrawl : MonoBehaviour {
	public float textSpeed;
	public Text textRef;
	public float textWidth;
	Vector3 reset;
	RectTransform canvasRect;
	// Use this for initialization
	void Start () {
		reset = transform.position;
		textRef = GetComponent<Text> ();
		textWidth = textRef.preferredWidth;
		canvasRect = GameObject.Find ("MainCanvas").GetComponent<RectTransform> ();
		
		print(textRef.rectTransform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
		print (canvasRect.rect.width);w
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			textRef.transform.position = reset;
		}
		if (textRef.rectTransform.offsetMin.x >= -textRef.rectTransform.rect.width) {
			//print (textRef.rectTransform.offsetMin.x);

			transform.position = transform.position + new Vector3 (-textSpeed * Time.deltaTime, 0, 0);
		}
	}
}
