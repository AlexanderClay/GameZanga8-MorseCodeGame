using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
	public float rate = 0.4f;
	public Vector2 gridPos;
	private Vector3 target;

	private Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start()
	{
		GameManager.objectTurnStack.Add(transform);

		transform.position = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
		target = transform.position;
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	public void FixedUpdate()
	{
		myRigidbody.MovePosition(Vector3.Lerp(transform.position, target, rate));
	}
	public void NewTurn()
	{

		Vector2 nextGridPos;
		nextGridPos = new Vector2(gridPos.x, gridPos.y + 2);

		gridPos = nextGridPos;

		target = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}
}
