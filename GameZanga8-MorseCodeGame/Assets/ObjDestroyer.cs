using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DestructionType
{
	Destroy,
	Deactivate
}

public class ObjDestroyer : MonoBehaviour {

	public DestructionType destructionType = DestructionType.Deactivate;
	// Use this for initialization
	void Start () {
		
	}

	private float timeToLive;

	public void DestroyCountdown(float time) {
		timeToLive = time;
		StartCoroutine(DestroyCountdown());
	}

	private IEnumerator DestroyCountdown()
	{
		float startTime = Time.time;

		while (Time.time < startTime + timeToLive) {
			yield return null;
		}
		Execute();
	}

	private void Execute()
	{
		if (destructionType == DestructionType.Deactivate) {

			gameObject.SetActive(false);
		} else if (destructionType == DestructionType.Destroy) {

			Destroy(gameObject);
		}
	}
}
