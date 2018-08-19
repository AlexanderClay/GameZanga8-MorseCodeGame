using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class MorseController : MonoBehaviour {

	public GameObject yourTurnText;
	public GameObject enemyTurnText;

	public AudioClip blipSound;
	public AudioClip wrongSound;
	public AudioClip correctSound;

	public GameObject morseDash;
	public GameObject morseDot;

	public MorseCodeList myMorseCodeList;

	public Transform morseCodeHighlighter1;
	public Transform morseCodeHighlighter2;
	public Transform morseCodeHighlighter3;
	public Transform morseCodeCanvasTransform;

	private string parsedMorse = "";
	public float dashCountdown = 0.06f;
	public float morseSendCountdown = 0.5f;
	// Use this for initialization

	private bool playedDash = false;
	// purely for audio polish
	private bool audioPlayedForLong = false;
	private bool canAcceptMorse = true;
	private bool startedClick = false;

	public List<int> alreadySelectedIndex = new List<int>();

	void Start () {
		yourTurnText.gameObject.SetActive(true);
		enemyTurnText.gameObject.SetActive(false);
	}
	
	private void SendMorse()
	{
		canAcceptMorse = false;
		StartCoroutine("DestroyMorseText");
		StartCoroutine("NextTurnCountdown");
		StopCoroutine("MorseSendCountdown");

		// compare morse
		yourTurnText.gameObject.SetActive(false);
		enemyTurnText.gameObject.SetActive(true);


		if (GameManager.levelNumber == 0) {
			if (parsedMorse != "...") {
				return;
			} else {
				GameManager.gameManagerObject.GetComponent<GameManager>().IncrementLevel();
				return;
			}
		}
		
		for (int i = 0; i < myMorseCodeList.morseCodeList.Count; i += 1) {
			if (myMorseCodeList.morseCodeList[i] == parsedMorse) {
				// space not shot before & has ammo
				
				if (GameManager.rocketsShot - GameManager.rocketLimit != 0) {

					if (alreadySelectedIndex.Contains(i) == true) {

						GameManager.SpawnAudioSource(wrongSound, 0.5f, 0.2f);
					}// else {

					//	GameManager.SpawnAudioSource(correctSound, 0.5f, 0.1f);
					//}

					alreadySelectedIndex.Add(i);
					GameManager.rocketCounter.ShootRocket();
					//GameManager.gameManagerObject.GetComponent<GameManager>().SpawnRocketParticlesForIndex(i);
					//GameManager.gameManagerObject.GetComponent<GameManager>().SpawnExplosionAtIndexWithDelay(i);
					GameManager.SpawnExplosionAtIndex(i);

				} else {

					GameManager.SpawnAudioSource(wrongSound, 0.5f, 0.2f);
				}
				
				if ((i + 1) <= 13) {
					// left side of transforms
					morseCodeHighlighter1.GetComponent<RectTransform>().anchoredPosition = morseCodeCanvasTransform.GetChild(i).localPosition; //+ new Vector3(-33f, 20f);
					morseCodeHighlighter1.GetComponent<Animator>().SetTrigger("anim");
				} else if ((i + 1) <= 26) {
					// left side of transforms
					morseCodeHighlighter2.GetComponent<RectTransform>().anchoredPosition = morseCodeCanvasTransform.GetChild(i).localPosition; //+ new Vector3(-28f, 20f);
					morseCodeHighlighter2.GetComponent<Animator>().SetTrigger("anim");
				} else {
					morseCodeHighlighter3.GetComponent<RectTransform>().anchoredPosition = morseCodeCanvasTransform.GetChild(i).localPosition; //+ new Vector3(-30f, 20f);
					morseCodeHighlighter3.GetComponent<Animator>().SetTrigger("anim");
				}

				return;
			}
		}

		GameManager.SpawnAudioSource(wrongSound, 0.5f, 0.2f);
	}
	IEnumerator NextTurnCountdown()
	{
		float startTime = Time.time;
		while (Time.time < startTime + 1.75f) {
			yield return null;
		}
		
		yourTurnText.gameObject.SetActive(true);
		enemyTurnText.gameObject.SetActive(false);
		GameManager.NewTurn();
		// can accept morse
		canAcceptMorse = true;
	}
	IEnumerator DestroyMorseText()
	{
		float startTime = Time.time;
		while (Time.time < startTime + 0.2f) {
			yield return null;
		}
		
		parsedMorse = "";
		foreach (Transform child in transform) {
			if (child.name != "Cursor") {

				Destroy(child.gameObject);
			}
		}
	}
	void Update ()
	{
		/*
		if (Input.GetKeyDown(KeyCode.KeypadEnter) == true || Input.GetKeyDown(KeyCode.Return) == true || Input.GetMouseButtonDown(1) == true) {
			SendMorse();
			StopCoroutine("MorseSendCountdown");
		}
		*/
		if (GameManager.levelNumber == 0 || GameManager.levelNumber == 7) {
			canAcceptMorse = true;
		}
		if (canAcceptMorse == false) {
			return;
		}


		if (Input.GetKeyDown(KeyCode.Space) == true || Input.GetMouseButtonDown(0) == true) {
			startedClick = true;
			GetComponent<AudioSource>().StopWebGL();
			GetComponent<AudioSource>().PlayWebGL(blipSound);

			StopCoroutine("StopSound");
			StopCoroutine("PlayedForAWhileCheck");
			StartCoroutine("PlayedForAWhileCheck");
			StopCoroutine("PlayedDashCheck");
			StartCoroutine("PlayedDashCheck");
			StopCoroutine("MorseSendCountdown");
			
			audioPlayedForLong = false;
			playedDash = false;
		}
		if (Input.GetKeyUp(KeyCode.Space) == true || Input.GetMouseButtonUp(0) == true) {
			if (startedClick == false) {
				return;
			}
			startedClick = false;

			StartCoroutine("MorseSendCountdown");

			if (playedDash == true) {
				parsedMorse += "-";
				GameObject obj = GameObject.Instantiate(morseDash, transform);
				obj.transform.SetSiblingIndex(transform.childCount - 2);
			} else {
				parsedMorse += ".";
				GameObject obj =  GameObject.Instantiate(morseDot, transform);
				obj.transform.SetSiblingIndex(transform.childCount - 2);
			}
			// checkMorse()
			if (parsedMorse.Length >= 5) {
				SendMorse();
			}

			if (audioPlayedForLong == true) {

				//print("Stop Imediately");
				GetComponent<AudioSource>().StopWebGL();
			} else {

				//print("Stop after 0.03f");
				StartCoroutine("StopSound");
			}
		}
	}
	IEnumerator MorseSendCountdown()
	{
		float startTime = Time.time;

		while (Time.time < startTime + morseSendCountdown) {
			yield return null;
		}


		SendMorse();
	}
	IEnumerator PlayedDashCheck()
	{
		float startTime = Time.time;

		while (Time.time < startTime + dashCountdown) {
			yield return true;
		}

		playedDash = true;
	}
	IEnumerator PlayedForAWhileCheck()
	{
		float startTime = Time.time;

		while (Time.time < startTime + 0.1f) {
			yield return true;
		}

		audioPlayedForLong = true;
	}
	IEnumerator StopSound()
	{
		float startTime = Time.time;

		while (Time.time < startTime + 0.05f) {
			yield return true;
		}


		GetComponent<AudioSource>().StopWebGL();
	}
}
