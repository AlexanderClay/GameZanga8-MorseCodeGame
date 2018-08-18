using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

	public float rate = 0.4f;
	public Vector2 gridPos;
	public bool dirLeft;

	private Vector3 target;
	private Rigidbody2D myRigidbody;

	void Start () {
		GameManager.objectTurnStack.Add(transform);

		if (dirLeft == true) {
			GetComponent<Animator>().SetTrigger("left");
		} else {
			GetComponent<Animator>().SetTrigger("right");
		}

		transform.position = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
		foreach (Transform trans in transform) {
			if (trans.GetComponent<TrailRenderer>() == true) {

				trans.GetComponent<TrailRenderer>().Clear();
			}
		}
		target = transform.position;
		myRigidbody = GetComponent<Rigidbody2D>();
	}

	public void Update()
	{
		myRigidbody.MovePosition(Vector3.Lerp(transform.position, target, rate));
	}

	public void NewTurn () {
		Vector2 nextGridPos;
		if (dirLeft == false) {
			nextGridPos = new Vector2(gridPos.x + 1, gridPos.y);
		} else {
			nextGridPos = new Vector2(gridPos.x - 1, gridPos.y);
		}
		
		if (nextGridPos.x <= -1) {
			dirLeft = false;
			GetComponent<Animator>().SetTrigger("right");
			print("turn right");
			nextGridPos = gridPos;
		} else if (nextGridPos.x >= 6) {
			dirLeft = true;
			GetComponent<Animator>().SetTrigger("left");
			print("turn left");
			nextGridPos = gridPos;
		}

		gridPos = nextGridPos;

		target = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}
}
