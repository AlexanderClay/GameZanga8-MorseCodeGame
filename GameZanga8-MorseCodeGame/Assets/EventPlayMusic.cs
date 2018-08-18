using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayMusic : MonoBehaviour {
	public AudioClip musicLoop;

	private void Start()
	{
		if (GameManager.hasPlayedMusic == false) {
			GameManager.hasPlayedMusic = true;
			GetComponent<AudioSource>().PlayWebGL();
		} else {
			Destroy(gameObject);
		}
	}
	void Update () {
		

		if (GetComponent<AudioSource>().isPlaying == false) {
			GetComponent<AudioSource>().loop = true;
			GetComponent<AudioSource>().PlayWebGL(musicLoop);
		}

		if (GetComponent<AudioSource>().volume <= 0.001f) {
			Destroy(gameObject);
		}


	}
}
