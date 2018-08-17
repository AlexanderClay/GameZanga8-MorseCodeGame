using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	
	
	// Update is called once per frame
	public void PlaySound () {
		GetComponent<AudioSource>().PlayWebGL();
	}
}
