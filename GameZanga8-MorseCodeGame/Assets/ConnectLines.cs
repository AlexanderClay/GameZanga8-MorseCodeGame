using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConnectLines : MonoBehaviour {
	public Transform target1;
	public Transform target2;
	// Use this for initialization
	void Update () {
		Vector3[] linePositions = new Vector3[2];
		linePositions[0] = target1.position;
		linePositions[1] = target2.position;

		GetComponent<LineRenderer>().SetPositions(linePositions);
	}
	
}
