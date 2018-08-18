using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static int levelNumber = 0;
	public static GameObject screenFadeIn;
	public static Pool audioPool;
	public static Pool explosionPool;
	public static List<Transform> objectTurnStack = new List<Transform>();

	private static Transform worldGridPositions;

	private void Awake ()
	{
		objectTurnStack.Clear();

		screenFadeIn = GameObject.Find("ScreenFadeIn");
		audioPool = transform.Find("AudioPool").GetComponent<Pool>();
		explosionPool = transform.Find("ExplosionPool").GetComponent<Pool>();
		worldGridPositions = GameObject.Find("WorldGridPositions").transform;
	}
	public static void Death()
	{
		screenFadeIn.GetComponent<Animator>().SetTrigger("death");
	}
	public static void SpawnAudioSource (AudioClip audioClipToPlay, float timeToLive) {

		GameObject audioObj = audioPool.Spawn(Vector3.zero);
		audioObj.GetComponent<AudioSource>().PlayWebGL(audioClipToPlay);
		audioObj.GetComponent<ObjDestroyer>().DestroyCountdown(timeToLive);
	}
	public static void SpawnExplosionAtIndex(int index)
	{
		Transform explosion = explosionPool.Spawn(worldGridPositions.GetChild(index).position).transform;
		objectTurnStack.Add(explosion);
	}
	public static Vector3 GetWorldPosFromIndex(int index)
	{
		
		return worldGridPositions.GetChild(index).position;
	}
	public static Vector3 GetWorldPosFromGrid(Vector2 gridPos, GameObject debugObject = null)
	{

		int x = (int) gridPos.x;
		int y = (int) gridPos.y;

		if (y <= -1) {
			y = 0;

		}
		if (y >= 7) {
			y = 6;

		}
		if (x <= -1) {
			x = 0;

		}
		if (x >= 6) {
			x = 5;

		}
		if (y >= 6) {
			Death();
		}

		int index = (x + (y * 6));
		
		return worldGridPositions.GetChild(index).position;
	}
	public static void NewTurn()
	{
		foreach(Transform obj in objectTurnStack) {
			if (obj.gameObject.activeSelf == true) {
				if (obj.GetComponent<PlaneController>() == true) {
					obj.GetComponent<PlaneController>().NewTurn();
				}
				if (obj.GetComponent<MeteorController>() == true) {
					obj.GetComponent<MeteorController>().NewTurn();
				}
			}
		}
	}
}
