using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	
	public AudioClip sfx;
	
	// Update is called once per frame
	public void PlaySound () {
		if (sfx == null) {

			GetComponent<AudioSource>().PlayWebGL();
		} else {

			GetComponent<AudioSource>().PlayWebGL(sfx);
		}
	}
}
