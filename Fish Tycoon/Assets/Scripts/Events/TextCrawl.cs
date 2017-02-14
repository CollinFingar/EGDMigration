using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCrawl : MonoBehaviour {
	public float textSpeed;
	public Text textRef;
	float textWidth; 
	int textSize; //current Text size, so if changed by best fit, it can re-size itself

	//Values that are reset upon text completion
	Vector3 reset;
	Vector2 maxStart;
	Vector2 minStart;

	RectTransform canvasRect;
	public bool textStart = false;
	public bool textFinish = false; //whether or not the text has finished moving and can be used again
	// Use this for initialization
	void Start () {
		textRef = GetComponent<Text> ();
		reset = transform.position;
		maxStart = textRef.rectTransform.offsetMax;
		minStart = textRef.rectTransform.offsetMin;
		textSize = 0;
		textWidth = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (textStart && !textFinish) {
			//resize textCrawl offset to compensate for resized screen (probably won't happen much, but for my sanity)
			if (textRef.cachedTextGenerator.fontSizeUsedForBestFit != textSize) {
				Font thisFont = textRef.font;
				int newWidth = 0;
				char[] textA = textRef.text.ToCharArray ();
				CharacterInfo characterInfo = new CharacterInfo ();
				foreach (char c in textA) {
					thisFont.GetCharacterInfo (c, out characterInfo, textRef.cachedTextGenerator.fontSizeUsedForBestFit);
					newWidth += characterInfo.advance;
				}
				textRef.rectTransform.offsetMax += new Vector2 (newWidth - textWidth, 0);
				textRef.rectTransform.offsetMin -= new Vector2 (newWidth - textWidth, 0);
				textWidth = newWidth;
				textSize = textRef.cachedTextGenerator.fontSizeUsedForBestFit;
				if (!textRef.enabled) {
					textRef.enabled = true;
				}
			}
			//print (textRef.cachedTextGenerator.fontSizeUsedForBestFit);
			if (textRef.rectTransform.offsetMin.x >= -textRef.rectTransform.rect.width) {
				//print (textRef.rectTransform.offsetMin.x);
				transform.position = transform.position + new Vector3 (-textSpeed * Time.deltaTime, 0, 0);
			} else {
				textRef.enabled = false;
				textFinish = true;
			}
		}
	}

	//This function is 
	public void TextSpawn(string crawlText,Color crawlColor) {
		textRef = GetComponent<Text> ();
		textRef.text = crawlText;
		textRef.color = crawlColor;
	}

	public void ScrollText(string crawlText,Color crawlColor) {
		textRef.text = crawlText;
		textRef.color = crawlColor;
		textStart = true;
		textFinish = false;
		//reset the text's position
		textSize = 0;
		textWidth = 0;
		transform.position = reset;
		textRef.rectTransform.offsetMax = maxStart;
		textRef.rectTransform.offsetMin = minStart;
	}
}
