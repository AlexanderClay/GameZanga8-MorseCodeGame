using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFirstTimeLoadingGame : MonoBehaviour {
	
	
	void Start () {
		if (GameManager.levelNumber == 0) {
			GetComponent<Animator>().SetTrigger("fade_out");
		}
	}
	
}
