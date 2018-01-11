using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {
	public GameObject playerSpawner,enemySpawner, controlsObject;
	public GameObject GameCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("j")) {
			playerSpawner.SetActive (true);
			enemySpawner.SetActive (true);
			controlsObject.SetActive (true);
			GameCanvas.SetActive (true);

			this.gameObject.SetActive (false);
		}
	}
}
