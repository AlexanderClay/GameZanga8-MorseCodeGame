using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTutorialHighlightS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameManager.levelNumber != 0) {
			Destroy(gameObject);
		}
	}
	
}
