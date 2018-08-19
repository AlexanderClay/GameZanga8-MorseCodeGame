using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour {

	private string savedText;
	
	string newText;

	void Start () {

		if (GameManager.levelNumber == 7) {
			newText = "Mission: [End!]";
		} else {
			newText = "Mission: [" + GameManager.levelNumber + " / 6]";
		}

		GetComponent<Text>().text = newText;
		/*
		if (newText != savedText) {
			savedText = newText;
			GetComponent<Text>().text = newText;
		}
		*/
	}
}
