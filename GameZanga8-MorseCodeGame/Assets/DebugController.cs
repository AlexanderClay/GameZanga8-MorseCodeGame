using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{

	public Vector2 gridPos;
	// Use this for initialization
	void Start()
	{
		GameManager.objectTurnStack.Add(transform);

		transform.position = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}

	public void Update()
	{
		transform.position = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}
}
