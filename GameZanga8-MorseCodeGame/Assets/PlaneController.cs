using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlaneController : MonoBehaviour {

	public float rate = 0.4f;
	public Vector2 gridPos;
	public bool dirLeft;
	public int gridSpeed = 1;

	public GameObject destructionSpawnObject;
	public int turnsUntilDroppingBombDefault;
	private int turnsUntilDroppingBomb;
	public GameObject bombObject;

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
	public void OnDestruction()
	{
		GameObject tempObj = GameObject.Instantiate(destructionSpawnObject, GameManager.gameManagerObject.GetComponent<GameManager>().currentWorld.transform);
		//tempObj.GetComponent<MeteorController>().skipThisTurn = true;
		tempObj.GetComponent<MeteorController>().gridPos = new Vector2(gridPos.x, gridPos.y + 1);
	}

	public void NewTurn() {

		// we must bomb at the start of the turn, not the end
		// if we are a bomber, and we're not on the edge, and there's no bomb at the position, try to drop a bomb

		if (bombObject != null && gridPos.x > 0 && gridPos.x < 5) {

			turnsUntilDroppingBomb -= 1;
			if (turnsUntilDroppingBomb == 0) {
				transform.Find("Bomb").gameObject.SetActive(false);
				GameObject tempObj = GameObject.Instantiate(bombObject, GameManager.gameManagerObject.GetComponent<GameManager>().currentWorld.transform);
				//tempObj.GetComponent<MeteorController>().skipThisTurn = true;
				tempObj.GetComponent<MeteorController>().gridPos = new Vector2(gridPos.x, gridPos.y + 1);
			}
		}

		Vector2 nextGridPos;

		if (dirLeft == false) {
			nextGridPos = new Vector2(gridPos.x + 1, gridPos.y);
		} else {
			nextGridPos = new Vector2(gridPos.x - 1, gridPos.y);
		}

		if (nextGridPos.x <= -1) {
			dirLeft = false;
			GetComponent<Animator>().SetTrigger("right");
			nextGridPos = new Vector2(gridPos.x, gridPos.y + 1);
		} else if (nextGridPos.x >= 6) {
			dirLeft = true;
			GetComponent<Animator>().SetTrigger("left");

			nextGridPos = new Vector2(gridPos.x, gridPos.y + 1);
		}


		gridPos = nextGridPos;

		target = GameManager.GetWorldPosFromGrid(gridPos, gameObject);
	}
}
