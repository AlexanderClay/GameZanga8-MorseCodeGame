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

		GetComponent<SpriteRenderer>().flipX = dirLeft;
		transform.position = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
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
		
		if (nextGridPos.x <= 0) {
			dirLeft = false;
			nextGridPos = gridPos;
		} else if (nextGridPos.x >= 5) {
			dirLeft = true;
			nextGridPos = gridPos;
		}

		gridPos = nextGridPos;

		GetComponent<SpriteRenderer>().flipX = dirLeft;
		target = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}
}
