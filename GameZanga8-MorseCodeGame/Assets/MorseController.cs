﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseController : MonoBehaviour {

	public GameObject morseDash;
	public GameObject morseDot;

	public MorseCodeList myMorseCodeList;

	public Transform morseCodeHighlighterLeft;
	public Transform morseCodeHighlighterRight;
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

	void Start () {
		
	}
	
	private void SendMorse()
	{
		canAcceptMorse = false;
		StartCoroutine("CanAcceptMorseAgainCountdown");

		GameManager.NewTurn();
		// compare morse

		for (int i = 0; i < myMorseCodeList.morseCodeList.Count; i += 1) {
			if (myMorseCodeList.morseCodeList[i] == parsedMorse) {
				
				if ((i + 1) <= 18) {
					// left side of transforms
					morseCodeHighlighterLeft.position = morseCodeCanvasTransform.GetChild(i).transform.position + new Vector3(-33f, 20f);
					morseCodeHighlighterLeft.GetComponent<Animator>().SetTrigger("anim");
				} else {
					morseCodeHighlighterRight.position = morseCodeCanvasTransform.GetChild(i+1).transform.position + new Vector3(-33f, 20f);
					morseCodeHighlighterRight.GetComponent<Animator>().SetTrigger("anim");
				}
			}
		}
	}
	IEnumerator CanAcceptMorseAgainCountdown()
	{
		float startTime = Time.time;
		while (Time.time < startTime + 0.2f) {
			yield return null;
		}

		canAcceptMorse = true;
		parsedMorse = "";
		foreach (Transform child in transform) {
			if (child.name != "Cursor") {

				Destroy(child.gameObject);
			}
		}
	}
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.KeypadEnter) == true || Input.GetKeyDown(KeyCode.Return) == true || Input.GetMouseButtonDown(1) == true) {
			SendMorse();
			StopCoroutine("MorseSendCountdown");
		}

		if (canAcceptMorse == false) {
			return;
		}


		if (Input.GetKeyDown(KeyCode.Space) == true || Input.GetMouseButtonDown(0) == true) {
			startedClick = true;
			GetComponent<AudioSource>().StopWebGL();
			GetComponent<AudioSource>().PlayWebGL();

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