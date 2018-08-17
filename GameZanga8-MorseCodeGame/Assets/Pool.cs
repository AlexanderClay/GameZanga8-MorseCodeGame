using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

	public GameObject templatePrefeb;
	private List<GameObject> pooledObjects = new List<GameObject>();
	public int createOnStart = 10;
	// Use this for initialization
	private void Start () {
		for (int i = 0; i < createOnStart; i += 1) {
			CreateNewObject();
		}
	}

	private GameObject CreateNewObject () {
		GameObject tempObj = GameObject.Instantiate(templatePrefeb);
		tempObj.SetActive(false);
		pooledObjects.Add(tempObj);

		return tempObj;
	}
	public GameObject Spawn(Vector3 newPos)
	{
		GameObject tempObj;
		for (int i = 0; i < pooledObjects.Count; i += 1) {
			if (pooledObjects[i].activeSelf == false) {
				tempObj = pooledObjects[i];
				tempObj.transform.position = newPos;
				tempObj.SetActive(true);
				return tempObj;
			}
		}

		print("no pooled object found. Creating new object");
		tempObj = CreateNewObject();
		tempObj.transform.position = newPos;
		tempObj.SetActive(true);
		return tempObj;
	}
}
