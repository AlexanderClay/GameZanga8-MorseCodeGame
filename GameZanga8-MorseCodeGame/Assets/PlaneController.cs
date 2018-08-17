using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

	public Vector2 gridPos;
	public float planeDir;
	void Start () {
		GameManager.objectTurnStack.Add(transform);
	}

	public void NewTurn () {
		if (planeDir == 0) {
			transform.position = GameManager.GetWorldPosFromGrid(new Vector2 (gridPos.x + 1, gridPos.y));
		} else {
			transform.position = GameManager.GetWorldPosFromGrid(new Vector2(gridPos.x - 1, gridPos.y));
		}
	}
}
