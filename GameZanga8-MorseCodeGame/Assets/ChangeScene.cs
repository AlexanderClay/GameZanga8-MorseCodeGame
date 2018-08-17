using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public string newScene;
	// Update is called once per frame
	public void ExecuteChangeScene () {
		SceneManager.LoadScene(newScene);
	}
}
