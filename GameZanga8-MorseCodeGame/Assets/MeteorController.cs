using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
	public bool canMove = false;
	public float rate = 0.4f;
	public int dropRate = 2;
	public Vector2 gridPos;
	private Vector3 target;

	private Rigidbody2D myRigidbody;
	// Use this for initialization
	private IEnumerator CanMoveAfterCreation()
	{
		float startTime = Time.time;
		while (Time.time < startTime + 0.6f) {
			yield return null;
		}

		canMove = true;
	}
	void Start()
	{
		GameManager.objectTurnStack.Add(transform);
		StartCoroutine("CanMoveAfterCreation");

		transform.position = GameManager.GetWorldPosFromGrid(gridPos, gameObject);

		foreach (Transform trans in transform) {
			if (trans.GetComponent<TrailRenderer>() == true) {

				trans.GetComponent<TrailRenderer>().Clear();
			}
		}

		target = transform.position;
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	public void FixedUpdate()
	{
		myRigidbody.MovePosition(Vector3.Lerp(transform.position, target, rate));
	}
	public void NewTurn()
	{
		if (canMove == false) {
			return;
		}
		Vector2 nextGridPos;
		nextGridPos = new Vector2(gridPos.x, gridPos.y + dropRate);

		gridPos = nextGridPos;

		target = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}
}
