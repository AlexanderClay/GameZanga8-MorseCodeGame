using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Pool audioPool;

	void Start () {
		audioPool = transform.Find("AudioPool").GetComponent<Pool>();
	}
	
	public static void SpawnAudioSource (AudioClip audioClipToPlay, float timeToLive) {

		GameObject audioObj = audioPool.Spawn(Vector3.zero);
		audioObj.GetComponent<AudioSource>().PlayWebGL(audioClipToPlay);
		audioObj.GetComponent<ObjDestroyer>().DestroyCountdown(timeToLive);
	}
}
