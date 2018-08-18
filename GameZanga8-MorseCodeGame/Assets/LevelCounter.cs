using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour {

	private string savedText;
	
	string newText;

	void Update () {

		newText = "[" + GameManager.levelNumber + " / 30]";
		if (newText != savedText) {
			savedText = newText;
			GetComponent<Text>().text = newText;
		}
	}
}
