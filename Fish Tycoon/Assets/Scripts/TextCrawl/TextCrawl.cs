using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCrawl : MonoBehaviour {
	public float textSpeed;
	public Text textRef;
	float textWidth; 
	int textSize; //current Text size, so if changed by best fit, it can re-size itself
	Vector3 reset;
	RectTransform canvasRect;
	// Use this for initialization
	void Start () {
		textSize = 0;
		textWidth = 0;
		reset = transform.position;
		textRef = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		//resize textCrawl offset to compensate for resized screen (probably won't happen much, but for my sanity)
		if (textRef.cachedTextGenerator.fontSizeUsedForBestFit != textSize) {
			Font thisFont = textRef.font;
			int newWidth = 0;
			char[] textA = textRef.text.ToCharArray ();
			CharacterInfo characterInfo = new CharacterInfo ();
			foreach (char c in textA) {
				thisFont.GetCharacterInfo (c, out characterInfo,textRef.cachedTextGenerator.fontSizeUsedForBestFit);
				newWidth += characterInfo.advance;
			}
			textRef.rectTransform.offsetMax += new Vector2 (newWidth - textWidth,0);
			textWidth = newWidth;
			textSize = textRef.cachedTextGenerator.fontSizeUsedForBestFit;
		}
		//print (textRef.cachedTextGenerator.fontSizeUsedForBestFit);
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			textRef.transform.position = reset;
		}
		if (textRef.rectTransform.offsetMin.x >= -textRef.rectTransform.rect.width) {
			//print (textRef.rectTransform.offsetMin.x);

			transform.position = transform.position + new Vector3 (-textSpeed * Time.deltaTime, 0, 0);
		}
	}
}
