using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour {

	private string savedText;
	
	string newText;

	void Start () {

		newText = "Mission: [" + GameManager.levelNumber + " / 30]";
		GetComponent<Text>().text = newText;
		/*
		if (newText != savedText) {
			savedText = newText;
			GetComponent<Text>().text = newText;
		}
		*/
	}
}
