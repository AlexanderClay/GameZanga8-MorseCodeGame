using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Pool audioPool;
	public static List<Transform> objectTurnStack = new List<Transform>();

	private static Transform worldGridPositions;
	void Start () {
		audioPool = transform.Find("AudioPool").GetComponent<Pool>();
		worldGridPositions = GameObject.Find("WorldGridPositions").transform;
	}
	
	public static void SpawnAudioSource (AudioClip audioClipToPlay, float timeToLive) {

		GameObject audioObj = audioPool.Spawn(Vector3.zero);
		audioObj.GetComponent<AudioSource>().PlayWebGL(audioClipToPlay);
		audioObj.GetComponent<ObjDestroyer>().DestroyCountdown(timeToLive);
	}
	public static Vector3 GetWorldPosFromIndex(int index)
	{
		
		return worldGridPositions.GetChild(index).position;
	}
	public static Vector3 GetWorldPosFromGrid(Vector2 gridPos)
	{
		int x = (int) gridPos.x;
		int y = (int) gridPos.x;

		int index = ((x + 1) * (y + 1)) - 1;

		return worldGridPositions.GetChild(index).position;
	}
	public static void NewTurn()
	{
		foreach(Transform obj in objectTurnStack) {
			if (obj.gameObject.activeSelf == true) {
				if (obj.GetComponent<PlaneController>() == true) {
					obj.GetComponent<PlaneController>().NewTurn();
				}
			}
		}
	}
}
