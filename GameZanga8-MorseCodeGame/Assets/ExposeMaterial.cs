using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExposeMaterial : MonoBehaviour {
	public bool animate = true;
	public Color color1;
	public float glowAlpha = 2.5f;

	private Renderer myRenderer;
	private MaterialPropertyBlock block;

	// Use this for initialization
	void Start () {
		block = new MaterialPropertyBlock();
		myRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (animate == true) {

			block.SetColor("_Color", color1);
			block.SetFloat("_MKGlowPower", glowAlpha);
			GetComponent<Renderer>().SetPropertyBlock(block);
		}
	}
}
