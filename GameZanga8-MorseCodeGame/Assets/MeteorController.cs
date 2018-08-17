using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
	public float rate = 0.4f;
	public Vector2 gridPos;
	private Vector3 target;
	// Use this for initialization
	void Start()
	{
		GameManager.objectTurnStack.Add(transform);

		transform.position = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
		target = transform.position;
	}
	public void Update()
	{
		transform.position = Vector3.Lerp(transform.position, target, rate);
	}
	public void NewTurn()
	{

		Vector2 nextGridPos;
		nextGridPos = new Vector2(gridPos.x, gridPos.y + 2);

		gridPos = nextGridPos;

		target = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}
}
