using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
	public Material[] colors;
	public float randomDelayMin;
	public float randomDelayMax;

	private MeshRenderer meshR;
	private int currentColor;

	// Use this for initialization
	void Start() {
		meshR = GetComponent<MeshRenderer>();
		meshR.material = colors[currentColor];
		StartCoroutine(changeToNextColor());
	}

	IEnumerator changeToNextColor() {
		while(true) {
			float seconds = Random.Range(randomDelayMin, randomDelayMax);
			yield return new WaitForSeconds(seconds);
			meshR.material = colors[++currentColor % colors.Length];
			if ((currentColor + 1) % colors.Length == 0) transform.gameObject.tag = "Red";
			else transform.gameObject.tag = "Green";
		}
	}
}
