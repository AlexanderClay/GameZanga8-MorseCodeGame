using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketCounter : MonoBehaviour {

	public GameObject rocketNumber;
	public GameObject outOfAmmoObject;

	void Start () {
		for (int i = 0; i < GameManager.rocketLimit; i +=1) {
			GameObject.Instantiate(rocketNumber, transform);
		}
	}
	public void ShootRocket()
	{
		GameManager.rocketsShot += 1;
		transform.GetChild(transform.childCount - GameManager.rocketsShot).GetComponent<Image>().color = Color.red;
		
	}
	public void OutOfAmmoAnimation()
	{
		outOfAmmoObject.GetComponent<CanvasGroup>().alpha = 1f;
	}

}
