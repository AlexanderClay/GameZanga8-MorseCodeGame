using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseController : MonoBehaviour {

	public GameObject morseDash;
	public GameObject morseDot;

	public MorseCodeList myMorseCodeList;
	private string parsedMorse = "";
	public float dashCountdown = 0.06f;
	// Use this for initialization

	private bool playedDash = false;
	// purely for audio polish
	private bool audioPlayedForLong = false;

	void Start () {
		
	}
	

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) == true) {
			GetComponent<AudioSource>().StopWebGL();
			GetComponent<AudioSource>().PlayWebGL();

			StopCoroutine("StopSound");
			StopCoroutine("PlayedForAWhileCheck");
			StartCoroutine("PlayedForAWhileCheck");
			StopCoroutine("PlayedDashCheck");
			StartCoroutine("PlayedDashCheck");

			audioPlayedForLong = false;
			playedDash = false;
		}
		if (Input.GetKeyUp(KeyCode.Space) == true) {

			if (playedDash == true) {
				parsedMorse += "-";
				GameObject.Instantiate(morseDash, transform);
			} else {
				parsedMorse += ".";
				GameObject.Instantiate(morseDot, transform);
			}
			// checkMorse()
			if (parsedMorse.Length >= 6) {

			}

			if (audioPlayedForLong == true) {

				print("Stop Imediately");
				GetComponent<AudioSource>().StopWebGL();
			} else {

				print("Stop after 0.03f");
				StartCoroutine("StopSound");
			}
		}
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
