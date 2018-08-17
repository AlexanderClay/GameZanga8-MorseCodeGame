using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VelocityLimitType
{
	Arcade,
	Vector
}

public class VelocityLimiter : MonoBehaviour {
	public VelocityLimitType myVelocityLimitType = VelocityLimitType.Vector;
	private Rigidbody2D myRigidbody;

	public float maxMagnitude;
	public Vector3 maxMagnitudeArcade;
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		if (myVelocityLimitType == VelocityLimitType.Vector) {
			myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxMagnitude);

		} else if (myVelocityLimitType == VelocityLimitType.Arcade) {
			myRigidbody.velocity = new Vector3(Mathf.Clamp(myRigidbody.velocity.x, -maxMagnitudeArcade.x, maxMagnitudeArcade.x),
				Mathf.Clamp(myRigidbody.velocity.y, -maxMagnitudeArcade.y, maxMagnitudeArcade.y),
				Mathf.Clamp(0f, -maxMagnitudeArcade.z, maxMagnitudeArcade.z));
		}
	}
}
