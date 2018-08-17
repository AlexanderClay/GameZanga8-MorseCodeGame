using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

	public float planeSpeed;
	void Start () {
		
	}
	
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().AddForce(planeSpeed * Vector2.left, ForceMode2D.Force);
	}
}
