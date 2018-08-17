using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceWebGL {

	/*
	 * 
	 * To Remove audio clicking noise
	 * 
	 */

	public static void StopWebGL(this AudioSource audioSource) {

		float audioSourceDefaultVolume = audioSource.volume;

		audioSource.volume = 0f;
		audioSource.Stop();

		audioSource.volume = audioSourceDefaultVolume;
	}

	public static void PlayWebGL(this AudioSource audioSource, AudioClip newAudioClip = null, float newVolume = -1f) {


		float audioSourceDefaultVolume = audioSource.volume;
		if (newVolume == -1f) {
			newVolume = audioSource.volume;
		}
		
		audioSource.volume = 0f;
		audioSource.Stop();

		if (newAudioClip != null) {
			audioSource.clip = newAudioClip;
		}

		if (newVolume == -1) {
			audioSource.volume = audioSourceDefaultVolume;
		} else {
			audioSource.volume = newVolume;
		}

		audioSource.Play();
	}
}
