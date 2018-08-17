using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FullscreenButton : MonoBehaviour, IPointerDownHandler {
	

	public void OnPointerDown(PointerEventData iData)
	{
		if (Screen.fullScreen == false) {

			Screen.fullScreen = true;
		}
	}
}
