using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public AudioClip outOfAmmoTurnClip;
	public static int levelNumber = 0; // 0
	public static int rocketLimit = 9;
	public static int rocketsShot = 0;
	public static int turnCount = 0;
	public bool playerIsDead = false;
	public bool playerIsSucceeded = false;
	public List<GameObject> worlds;
	public List<int> worldRocketLimit;
	private bool outOfAmmoAnimation = false;
	private bool succeededAnimation = false;
	public GameObject currentWorld;

	public static RocketCounter rocketCounter;

	public static MorseController morseController;
	private Transform rocketsParticleSystem;
	public static GameObject gameManagerObject;
	public static GameObject screenFadeIn;
	public static Pool audioPool;
	public static Pool explosionPool;
	public static List<Transform> objectTurnStack = new List<Transform>();

	private static Transform worldGridPositions;

	private void Awake ()
	{
		objectTurnStack.Clear();
		turnCount = 0;
		rocketsShot = 0;
		SpawnCurrentLevel();

		gameManagerObject = gameObject;
		rocketCounter = GameObject.Find("Canvas/CommandEnter/RocketCounter").GetComponent<RocketCounter>();
		morseController = GameObject.Find("Canvas/CommandEnter/MorseLine").GetComponent<MorseController>();

		screenFadeIn = GameObject.Find("ScreenFadeIn");
		rocketsParticleSystem = transform.Find("RocketsParticleSystem");
		audioPool = transform.Find("AudioPool").GetComponent<Pool>();
		explosionPool = transform.Find("ExplosionPool").GetComponent<Pool>();
		worldGridPositions = GameObject.Find("WorldGridPositions").transform;
	}
	public void Update()
	{
		CheckOutofAmmoUpdate();
		CheckSucceededUpdate();

	}
	public void CheckOutofAmmoUpdate()
	{
		if (outOfAmmoAnimation == true || levelNumber == 0 || playerIsSucceeded == true) {
			return;
		}
		if (turnCount != 0 && rocketsShot == rocketLimit) {
			outOfAmmoAnimation = true;
			rocketCounter.OutOfAmmoAnimation();
			StartCoroutine("OutOfAmmoStartDelay");
		}
	}
	private IEnumerator OutOfAmmoStartDelay()
	{
		float startTime = Time.time;

		while (Time.time < startTime + 1f) {
			yield return null;
		}

		if (playerIsDead == false && playerIsSucceeded == false) {
			StartCoroutine("OutOfAmmo");
		}
	}
	private IEnumerator OutOfAmmo()
	{
		float startTime = Time.time;

		while (Time.time < startTime + 0.4f) {
			yield return null;
		}

		SpawnAudioSource(outOfAmmoTurnClip, 0.5f);
		NewTurn();
		if (playerIsDead == false && playerIsSucceeded == false) {
			StartCoroutine("OutOfAmmo");
		}
	}
	public void CheckSucceededUpdate()
	{

		if (succeededAnimation == true || levelNumber == 0 || playerIsDead == true) {
			return;
		}

		// check all enemies in this level if they are alive
		foreach (Transform child in currentWorld.transform) {
			if (child.gameObject.activeSelf == true) {
				return;
			}
		}
		// else, all enemies dead:
		IncrementLevel();
	}
	public void IncrementLevel()
	{
		succeededAnimation = true;
		playerIsSucceeded = true;
		levelNumber += 1;
		screenFadeIn.GetComponent<Animator>().SetTrigger("end_level");
	}
	public void SpawnCurrentLevel()
	{
		for (int i = 0; i < worlds.Count; i += 1) {
			worlds[i].SetActive(false);
		}

		rocketLimit = worldRocketLimit[levelNumber];

		currentWorld = worlds[levelNumber];
		currentWorld.SetActive(true);
	}
	public static void Death()
	{
		gameManagerObject.GetComponent<GameManager>().playerIsDead = true;
		screenFadeIn.GetComponent<Animator>().SetTrigger("death");
	}
	public static void SpawnAudioSource (AudioClip audioClipToPlay, float timeToLive, float volume = 1f) {

		GameObject audioObj = audioPool.Spawn(Vector3.zero);
		audioObj.GetComponent<AudioSource>().PlayWebGL(audioClipToPlay, volume);
		audioObj.GetComponent<ObjDestroyer>().DestroyCountdown(timeToLive);
	}
	public static void SpawnExplosionAtPosition(Vector3 pos)
	{
		Transform explosion = explosionPool.Spawn(pos).transform;
		objectTurnStack.Add(explosion);
	}
	public static void SpawnExplosionAtIndex(int index)
	{
		Transform explosion = explosionPool.Spawn(worldGridPositions.GetChild(index).position).transform;
		//objectTurnStack.Add(explosion);
	}
	public void SpawnExplosionAtIndexWithDelay(int index)
	{
		StartCoroutine("ExplosionDelay", index);
		//objectTurnStack.Add(explosion);
	}
	private IEnumerator ExplosionDelay(int index)
	{
		float startTime = Time.time;

		while (Time.time < startTime + 0.4f) {

			yield return null;
		}


		Transform explosion = explosionPool.Spawn(worldGridPositions.GetChild(index).position).transform;
	}

	public void SpawnRocketParticlesForIndex(int index)
	{
		Vector3 targetPosition = worldGridPositions.GetChild(index).position;
		rocketsParticleSystem.position = new Vector3(targetPosition.x, targetPosition.y - 17.5f, 0f);

		rocketsParticleSystem.GetComponent<ParticleSystem>().Play();

	}
	public static int GetIndexFromGridPos(Vector2 pos)
	{

		return (int)(pos.x + (pos.x * 6));
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
		int index = (x + (y * 6));

		if (y >= 6) {
			// death
		}

		return worldGridPositions.GetChild(index).position;
	}
	public static void NewTurn()
	{
		turnCount += 1;
		foreach (Transform obj in objectTurnStack) {
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
