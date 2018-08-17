using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DebugMorseCodeGrabber : MonoBehaviour {
	public MorseCodeList myMorseCodeList;
	// Use this for initialization
	void Start () {

		string textStrings = "";

		for (int i = 0; i < myMorseCodeList.morseCodeList.Count; i+= 1) {
			textStrings += myMorseCodeList.morseCodeList[i] + "\n";
		}
		print(textStrings);
		GetComponent<Text>().text = textStrings;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
