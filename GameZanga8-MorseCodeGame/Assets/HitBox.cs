using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

	public AudioClip explosionClip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void OnTriggerEnter2D (Collider2D other) {

		GameManager.SpawnAudioSource(explosionClip, 1f);
		other.gameObject.SetActive(false);
	}
}
