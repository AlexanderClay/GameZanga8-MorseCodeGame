using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHitBox : MonoBehaviour {
	
	void Start () {
		
	}
	
	// Update is called once per frame
	public void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<MeteorController>() || other.GetComponent<PlaneController>()) {

			GameManager.SpawnExplosionAtPosition(other.transform.position);
			GameManager.Death();
		}
	}
}
